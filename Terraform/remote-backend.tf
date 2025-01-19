# store the terraform state file in s3 and lock with dynamodb
terraform {
  backend "s3" {
    bucket         = "cmsteam5-terraform-remote-state"
    key            = "Contract_Management_V1-main/terraform.tfstate"
    region         = "us-east-1"
    dynamodb_table = "cmsteam5-state-lock"
  }
}