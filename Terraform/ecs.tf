# ECS Cluster
resource "aws_ecs_cluster" "main" {
  name = "Team5Cluster"
}

# ECS Task Definition
resource "aws_ecs_task_definition" "app" {
  family                   = "Team5CMSTask"
  network_mode             = "awsvpc" 
  requires_compatibilities = ["FARGATE"] 
  cpu                      = "2048" 
  memory                   = "1024" 

  container_definitions = jsonencode([
    {
      name        = "Team5contract-app"
      image       = "nginx:latest" # Replace with your application image
      cpu         = 256
      memory      = 512
      essential   = true
      portMappings = [
        {
          containerPort = 80
          hostPort      = 80
          protocol      = "tcp"
        }
      ]
      logConfiguration = {
        logDriver = "awslogs"
        options = {
          "awslogs-group"         = "/ecs/contract-management"
          "awslogs-region"        = var.region
          "awslogs-stream-prefix" = "ecs"
        }
      }
    },
    {
      name        = "contract-api"
      image       = "amazonlinux:latest" # Replace with your API image
      cpu         = 256
      memory      = 512
      essential   = true
      portMappings = [
        {
          containerPort = 3000
          hostPort      = 3000
          protocol      = "tcp"
        }
      ]
      environment = [
        {
          name  = "DB_HOST"
          value = aws_db_instance.main.endpoint
        },
        {
          name  = "DB_PORT"
          value = "3306"
        },
        {
          name  = "DB_USER"
          value = "admin"
        },
        {
          name  = "DB_PASSWORD"
          value = var.rds_password
        }
      ]
      logConfiguration = {
        logDriver = "awslogs"
        options = {
          "awslogs-group"         = "/ecs/contract-management"
          "awslogs-region"        = var.region
          "awslogs-stream-prefix" = "ecs"
        }
      }
    }
  ])

  execution_role_arn = aws_iam_role.ecs_task_execution_role.arn
  task_role_arn      = aws_iam_role.ecs_task_execution_role.arn
}

# ECS Service
resource "aws_ecs_service" "app" {
  name            = "ContractManagementService"
  cluster         = aws_ecs_cluster.main.id
  task_definition = aws_ecs_task_definition.app.arn
  desired_count   = 2
  launch_type     = "FARGATE"

  network_configuration {
    subnets         = [aws_subnet.private.id]
    security_groups = [aws_security_group.ecs.id]
    assign_public_ip = false
  }

  load_balancer {
    target_group_arn = aws_lb_target_group.ecs.arn
    container_name   = "contract-app"
    container_port   = 80
  }

  depends_on = [aws_lb_listener.https]
}
