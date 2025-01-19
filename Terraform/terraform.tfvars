# environment variables
region       = "us-east-1"
project_name = "cmsteam5"
environment  = "dev"

# vpc variables
vpc_cidr                     = "10.0.0.0/16"
public_subnet_az1_cidr       = "10.0.0.0/24"
public_subnet_az2_cidr       = "10.0.1.0/24"
private_app_subnet_az1_cidr  = "10.0.2.0/24"
private_app_subnet_az2_cidr  = "10.0.3.0/24"
private_data_subnet_az1_cidr = "10.0.4.0/24"
private_data_subnet_az2_cidr = "10.0.5.0/24"

# rds variables
multi_az_deployment          = "false"
database_instance_identifier = "app-db"
database_instance_class      = "db.t3.micro"
publicly_accessible          = "false"
username                     = "postgres"
db-password                     = "postgres"
rds_db_name                  = "postgres"

# acm variables
domain_name       = "effiecancode.buzz"
alternative_names = "www.effiecancode.buzz"
certificate_arn = ""

# route-53 variables
record_name = "effiecancode.buzz"

# ec2 variables
ec2_key_name = "cms-t5"
ec2_instance_type = "t2-micro"
