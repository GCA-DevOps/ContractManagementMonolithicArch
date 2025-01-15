resource "aws_db_instance" "main" {
  allocated_storage    = 20
  engine               = "mysql"
  engine_version       = "8.0"
  instance_class       = "db.t3.medium"
  name                 = "contractdb"
  username             = "admin"
  password             = var.rds_password
  publicly_accessible  = false
  vpc_security_group_ids = [aws_security_group.rds.id]
  subnet_group_name    = aws_db_subnet_group.main.name
}

resource "aws_db_subnet_group" "main" {
  name       = "main-db-subnet-group"
  subnet_ids = [aws_subnet.private.id]

  tags = {
    Name = "DBSubnetGroup"
  }
}
