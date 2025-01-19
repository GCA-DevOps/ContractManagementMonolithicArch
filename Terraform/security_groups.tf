# alb SGs
resource "aws_security_group" "alb" {
  name        = "team5ALBSecurityGroup"
  vpc_id      = aws_vpc.main.id

  ingress {
    from_port   = 80
    to_port     = 80
    protocol    = "tcp"
    cidr_blocks = ["0.0.0.0/0"]
  }

  ingress {
    from_port   = 443
    to_port     = 443
    protocol    = "tcp"
    cidr_blocks = ["0.0.0.0/0"]
  }

  egress {
    from_port   = 0
    to_port     = 0
    protocol    = "-1"
    cidr_blocks = ["0.0.0.0/0"]
  }

  tags = {
    Name = "team5ALBSecurityGroup"
  }
}

# ec2 SGs
resource "aws_security_group" "ec2_sg" {
  name        = "team5ec2securitygroup"
  description = "Allow inbound HTTP, HTTPS, and RDP traffic"

  ingress {
    description = "Allow RDP"
    from_port   = 3389
    to_port     = 3389
    protocol    = "tcp"
    cidr_blocks = ["0.0.0.0/0"]
  }

  ingress {
    description = "Allow HTTP"
    from_port   = 80
    to_port     = 80
    protocol    = "tcp"
    cidr_blocks = ["0.0.0.0/0"]
  }

  ingress {
    description = "Allow HTTPS"
    from_port   = 443
    to_port     = 443
    protocol    = "tcp"
    cidr_blocks = ["0.0.0.0/0"]
  }

  egress {
    description = "Allow all outbound traffic"
    from_port   = 0
    to_port     = 0
    protocol    = "-1"
    cidr_blocks = ["0.0.0.0/0"]
  }

  tags = {
    Name = "team5ec2SecurityGroup"
  }
}

# rds SGs
resource "aws_security_group" "rds" {
  name        = "team5RDSSecurityGroup"
  vpc_id      = aws_vpc.main.id

  # Ingress: Allow traffic from ECS security group on port 3306 (MySQL/PostgreSQL default)
  ingress {
    from_port       = 3306
    to_port         = 3306
    protocol        = "tcp"
    security_groups = [aws_security_group.ecs.id]
  }

  # Egress: Allow traffic only back to the ECS security group on port 3306
  egress {
    from_port       = 3306
    to_port         = 3306
    protocol        = "tcp"
    security_groups = [aws_security_group.ecs.id]
  }

  tags = {
    Name = "team5RDSSecurityGroup"
  }
}
