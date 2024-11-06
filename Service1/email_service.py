# email_service.py
import smtplib
from email.mime.multipart import MIMEMultipart
from email.mime.text import MIMEText

# Email server configuration
EMAIL_HOST = 'smtp.gmail.com'
EMAIL_PORT = 587
EMAIL_HOST_USER = 'chinmay.je*************8.com'
# EMAIL_HOST_USER = 'travel.exploooration@gmail.com'
EMAIL_HOST_PASSWORD = '*************'


def send_email(subject, content, recipient=EMAIL_HOST_USER):
    # Set up the email
    message = MIMEMultipart()
    message["From"] = EMAIL_HOST_USER
    message["To"] = recipient
    message["Subject"] = subject

    # Attach the text content
    message.attach(MIMEText(content, "plain"))  # Change to "plain" for simple text

    try:
        # Connect to the SMTP server and send the email
        with smtplib.SMTP(EMAIL_HOST, EMAIL_PORT) as server:
            server.starttls()
            server.login(EMAIL_HOST_USER, EMAIL_HOST_PASSWORD)
            server.sendmail(EMAIL_HOST_USER, recipient, message.as_string())
    except Exception as e:
        print(f"Error sending email: {e}")


# def send_email(subject, html_content, recipient=EMAIL_HOST_USER):
#     # Set up the email
#     message = MIMEMultipart("alternative")
#     message["From"] = EMAIL_HOST_USER
#     message["To"] = recipient
#     message["Subject"] = subject

#     # Attach the HTML content
#     message.attach(MIMEText(html_content, "html"))

#     try:
#         # Connect to the SMTP server and send the email
#         with smtplib.SMTP(EMAIL_HOST, EMAIL_PORT) as server:
#             server.starttls()
#             server.login(EMAIL_HOST_USER, EMAIL_HOST_PASSWORD)
#             server.sendmail(EMAIL_HOST_USER, recipient, message.as_string())
#         # print("Email sent successfully!")
#     except Exception as e:
#         print(f"Error sending email: {e}")
        
# if __name__ == "__main__":
#     send_email(
#         subject="Test Email",
#         html_content="<h1>This is a test email</h1><p>Hello, this is a test!</p>",
#         recipient="chinmay.jena7878@gmail.com"
#     )        


if __name__ == "__main__":
    send_email(
        subject="Test Email",
        content="This is a test email content.",
        recipient="chinmay.jena7878@gmail.com"
    )
