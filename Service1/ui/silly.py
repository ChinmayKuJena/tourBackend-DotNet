import streamlit as st
import requests
import json
from streamlit_lottie import st_lottie

# Function to load Lottie animation from a local file
def load_lottie_local(file_path: str):
    try:
        with open(file_path, "r") as file:
            return json.load(file)
    except FileNotFoundError:
        st.error(f"Lottie animation file not found at {file_path}.")
        return None

# Set the title of the app
st.set_page_config(page_title="Chinmay Kumar Jena's Portfolio", layout="wide")

# Create two columns for layout
col1, col2 = st.columns([3, 1])  # Adjust column widths as needed

# Header Section in the first column
with col1:
    st.markdown("<h1 style='color: orange;'>Chinmay Kumar Jena</h1>", unsafe_allow_html=True)
    st.subheader("Spring Boot Backend Developer")
    st.write("📧 Email: [chinmay.jena7878@gmail.com](mailto:chinmay.jena7878@gmail.com)")
    st.write("📍 Location: Balasore, Odisha")
    st.write("📞 Phone: [8926215167](tel:+918926215167)")
    st.write("[LinkedIn Profile](https://linkedin.com/in/chinmay-jena-0234642a0)")

# Hulk Animation in the second column
with col2:
    hulk_animation = load_lottie_local(r'C:\ALL_PROJECTS\personal\dot-net\Service1\ui\Animation - 1730617140670.json')  # Update the path to your Hulk animation
    if hulk_animation:
        st_lottie(hulk_animation, speed=1, width=200, height=200, key="hulk")  # Adjust size as needed

# Experience Section
st.markdown("<h2 style='color: orange;'>Experience</h2>", unsafe_allow_html=True)
st.write("### Skytreesoft, Canada — Spring Boot Backend Developer (Intern)")
st.write("June 2024 - PRESENT")
st.write("""
- Developed and optimized Spring Boot services to enhance application performance.
- Designed AWS infrastructure including API Gateway, SQS, and RDS.
- Built continuous polling services from AWS SQS for integration with machine learning models.
- Implemented advanced web scraping techniques and integrated OpenAI's GPT-based models for automation.
""")

# Projects Section
st.markdown("<h2 style='color: orange;'>Projects</h2>", unsafe_allow_html=True)
st.write("### Tourism Data Aggregation and Microservices API")
st.write("Developing a microservices-based platform for aggregating and delivering tourism data.")
st.write("""
- **User Management & API Service**: Secure user registration and authentication (JWT).
- **Dataset Service**: Manages location data from external APIs like GeoNames and Nominatim.
- **GPT Integration (In Progress)**: Aims to provide AI-driven insights, including historical information and notable attractions.
""")
st.write("Demo: [View Demo](https://chinmay09-tour-phase-1.streamlit.app/)")

# Skills Section
st.markdown("<h2 style='color: orange;'>Skills</h2>", unsafe_allow_html=True)
skills = {
    "Programming Languages": ["Java", "Python"],
    "Database": ["PostgreSQL", "MySQL", "MongoDB"],
    "Frameworks": ["Spring Boot", "Django"],
    "Cloud": ["AWS"]
}
for skill_category, skill_list in skills.items():
    st.write(f"**{skill_category}:** {', '.join(skill_list)}")

# Education Section
st.markdown("<h2 style='color: orange;'>Education</h2>", unsafe_allow_html=True)
st.write("### Bachelor of Technology in Computer Science and Engineering")
st.write("Modern Engineering and Management Studies, Balasore (Expected Graduation: 2026)")
st.write("### Higher Secondary Education")
st.write("Fakir Mohan Junior College, Balasore (2020 - 2022)")
st.write("### Secondary Education")
st.write("Esteem Public School, Jharkhand (2020)")

# Languages Section
st.markdown("<h2 style='color: orange;'>Languages</h2>", unsafe_allow_html=True)
languages = ["English", "Hindi", "Odia"]
st.write(", ".join(languages))

# Footer
st.markdown("---")
st.write("Built with ❤️ using Streamlit")

# Add some animations or styling
# st.balloons()  # Display balloons animation
