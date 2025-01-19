terraform {
  required_providers {
    aws = {
      source  = "hashicorp/aws"
      version = "~> 5.0"
    }
  }
  required_version = ">= 1.4.0"
}

# provider "aws" {
#   access_key = "ASIAXVEABA2AL4XGME5A"
#   secret_key = "AIYqp3KLeTQ4dhWR8/NGa3nQ6tChNSKJbZtpkSBp"
#   region     = "us-east-1"
# }
