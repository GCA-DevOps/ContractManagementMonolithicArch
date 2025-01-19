output "vpc_id" {
  value = aws_vpc.main.id
}

output "alb_dns_name" {
  value = aws_lb.main.dns_name
}

output "rds_endpoint" {
  value = aws_db_instance.main.endpoint
}


# rds endpoint
output "rds_endpoint" {
  value = aws_db_instance.database_instance.endpoint
}

### outputs needed to create self-hosted aws ec2 runner
# private data subnet az1 id
output "private_data_subnet_az1_id" {
  value = aws_subnet.private_data_subnet_az1.id
}


# website url
output "website_url" {
  value = join("", ["https://", var.record_name, ".", var.domain_name])
}
