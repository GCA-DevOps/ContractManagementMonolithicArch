# Route 53 Hosted Zone (Assumes you already have the domain)
data "aws_route53_zone" "primary" {
  name = "wachiyecloud.cc."
}

# A Record for the Application
resource "aws_route53_record" "app" {
  zone_id = data.aws_route53_zone.primary.zone_id
  name    = "app"
  type    = "A"

  alias {
    name                   = aws_lb.main.dns_name
    zone_id                = aws_lb.main.zone_id
    evaluate_target_health = true
  }
}
