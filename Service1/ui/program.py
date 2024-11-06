from datetime import datetime
import os
import pytz
import streamlit as st
import requests
from dotenv import load_dotenv

# Load environment variables from .env file
load_dotenv()

# Fetch the base API URL from the environment variable
BASE_URL = os.getenv("BASE_URL", "http://localhost:5179/api")

# Set page configuration and title
st.set_page_config(page_title="Place Info and Comparison Fetcher", page_icon="🗺️", layout="wide")

# Function to generate greeting based on the user's time zone
def get_greeting():
    tz = pytz.timezone("Asia/Kolkata")
    current_time = datetime.now(tz)
    current_hour = current_time.hour
    current_day = current_time.strftime("%A")

    if 5 <= current_hour < 12:
        greeting = "Good Morning"
    elif 12 <= current_hour < 17:
        greeting = "Good Afternoon"
    elif 17 <= current_hour < 21:
        greeting = "Good Evening"
    else:
        greeting = "Good Night"

    return f"{greeting}, Happy {current_day}!"

# Function to fetch data from the API
def fetch_data(endpoint, place):
    try:
        url = f"{BASE_URL}/{endpoint}/{place}"
        response = requests.get(url)
        response.raise_for_status()
        return response.json()
    except requests.exceptions.HTTPError as http_err:
        print(f"HTTP error occurred: {http_err}")
        st.error(f"HTTP error occurred")
    except Exception as err:
        print(f"An error occurred: {err}")
        st.error(f"An error occurred")

# Function to fetch images for a place
def fetch_place_images(place):
    try:
        url = f"{BASE_URL}/getPlaceImages/{place}"
        response = requests.get(url, timeout=5)
        response.raise_for_status()
        data = response.json()
        image_urls = [result['s3Url'] for result in data if result.get('isPlaceImage')]
        return image_urls
    except Exception as err:
        # st.warning("An error occurred while fetching images.")
        return []

# Function to upload an image
def upload_image(image_file, place_name):
    url = f"{BASE_URL}/recognize"
    try:
        files = {'file': image_file}
        data = {'placeName': place_name}
        response = requests.post(url, files=files, data=data)
        response.raise_for_status()
        return response.json()
    except Exception as e:
        st.error(f"Error uploading image: {str(e)}")
        return None

# Function to compare places
def compare_places(places, criteria):
    url = f"{BASE_URL}/compare"
    payload = {"Places": places, "Criteria": criteria}
    response = requests.post(url, json=payload)
    if response.status_code == 200:
        return response.json()
    else:
        st.error("Error fetching data from the API.")
        return []

# Display greeting
st.markdown(f"<h3 style='color:orange;'>{get_greeting()}</h3>", unsafe_allow_html=True)

# Sidebar for navigation with page options
st.sidebar.title("Navigation")
pages = {
    "Place Details": "details",
    "Historical Facts": "historical_facts",
    "Attractions": "attractions",
    "Famous Places": "famous_places",
    "Unique Information": "unique_information",
    "Compare Places": "compare",
    "Trivia" : "trivia",
    "Upload Image": "upload_image"
}
selected_page = st.sidebar.selectbox("Select a page", list(pages.keys()))

# Section for fetching place information
if selected_page in ["Place Details", "Historical Facts", "Attractions", "Famous Places", "Unique Information","Trivia"]:
    st.markdown(f"<h2 style='color:orange;'>{selected_page}</h2>", unsafe_allow_html=True)
    with st.form(key='place_form'):
        place = st.text_input("Enter a place name 👇", placeholder="Type Here")
        submit_button = st.form_submit_button("Get Information")

    if place and submit_button:
        endpoint = pages[selected_page]
        with st.spinner("Fetching information..."):
            data = fetch_data(endpoint, place)
        
        if data:
            st.subheader(f"{selected_page} for '{place}':")
            if selected_page == "Place Details" and "details" in data:
                details = data["details"]
                for key, value in details.items():
                    st.markdown(f"<span style='font-weight:bold; color:orange;'>{key.replace('_', ' ').title()}</span>: {value}", unsafe_allow_html=True)
                
                # Fetch images for the place after displaying details
                with st.spinner("Fetching images..."):
                    images = fetch_place_images(place)
                    if images:
                        st.markdown("<h3 style='color:orange;'>Images:</h3>", unsafe_allow_html=True)
                        cols = st.columns(3)  # Display 3 images per row
                        for index, image_url in enumerate(images):
                            with cols[index % 3]:
                                st.image(image_url, caption=f"Image of {place}", use_column_width=True)
                    else:
                        st.warning("No images found.")
                        # if st.button("Be the first to upload an image"):
                        #     st.session_state.selected_page = "Upload Image"
                        #     st.experimental_rerun()
                        
            else:
                key = pages[selected_page]                        
                st.markdown(f"<span style='font-weight:bold; color:orange;'>{selected_page}</span>: {data.get(key, 'No details available.')}", unsafe_allow_html=True)    
# # # Section for comparing places
# # if selected_page == "Compare Places":
# #     st.markdown('<h2 class="sub-title">Compare Places</h2>', unsafe_allow_html=True)
# #     place1 = st.text_input("Enter the first place name", placeholder="e.g., Odisha", key="place1")
# #     place2 = st.text_input("Enter the second place name", placeholder="e.g., Pune", key="place2")
# #     criteria_options = ["historical_facts", "attractions", "unique_information"]
# #     selected_criteria = st.multiselect("Criteria", criteria_options, key="criteria")
# #     compare_button = st.button("Compare Places")

# #     if compare_button:
# #         if not place1 or not place2:
# #             st.error("Both place names are required for comparison.")
# #         elif not selected_criteria:
# #             st.error("At least one criterion must be selected for comparison.")
# #         else:
# #             places = [place1, place2]
# #             with st.spinner("Comparing places..."):
# #                 comparison_data = compare_places(places, selected_criteria)
                
# #                 if comparison_data:
# #                     st.subheader("Comparison Results:")
# #                     for result in comparison_data:
# #                         place_name = result.get('placeName', 'Unknown Place')
# #                         details = result.get('details', {})
# #                         details_text = ', '.join([f"{k}: {v}" for k, v in details.items()]) or "No details available."
# #                         st.markdown(f"<div><strong>{place_name}</strong>: {details_text}</div>", unsafe_allow_html=True)
# #                 else:
# #                     st.warning("No comparison data found.")

# # Section for comparing places
# if selected_page == "Compare Places":
#     st.markdown('<h2 class="sub-title">Compare Places</h2>', unsafe_allow_html=True)
#     place1 = st.text_input("Enter the first place name", placeholder="e.g., Odisha", key="place1")
#     place2 = st.text_input("Enter the second place name", placeholder="e.g., Pune", key="place2")
#     criteria_options = ["historical_facts", "attractions", "unique_information"]
#     selected_criteria = st.multiselect("Criteria", criteria_options, key="criteria")
#     compare_button = st.button("Compare Places")

#     if compare_button:
#         if not place1 or not place2:
#             st.error("Both place names are required for comparison.")
#         elif not selected_criteria:
#             st.error("At least one criterion must be selected for comparison.")
#         else:
#             places = [place1, place2]
#             with st.spinner("Comparing places..."):
#                 comparison_data = compare_places(places, selected_criteria)

#                 if comparison_data:
#                     # Extract data for table
#                     table_data = []
#                     columns = ["Criterion", f"{place1}", f"{place2}"]
                    
#                     for criterion in selected_criteria:
#                         row = [criterion.capitalize()]
#                         for place in [place1, place2]:
#                             # Find details for each place and criterion
#                             place_data = next((item for item in comparison_data if item["placeName"] == place), {})
#                             details = place_data.get("details", {}).get(criterion, "No data available")
#                             row.append(details)
#                         table_data.append(row)

#                     # Display the table
#                     st.markdown("### Comparison Results")
#                     st.markdown(
#                         f"<table style='width:100%; border: 1px solid orange;'>"
#                         f"<tr><th>{columns[0]}</th><th>{columns[1]}</th><th>{columns[2]}</th></tr>" +
#                         "".join(
#                             f"<tr><td>{row[0]}</td><td>{row[1]}</td><td>{row[2]}</td></tr>"
#                             for row in table_data
#                         ) +
#                         "</table>",
#                         unsafe_allow_html=True
#                     )
#                 else:
#                     st.warning("No comparison data found.")


# Section for comparing places
if selected_page == "Compare Places":
    st.markdown('<h2 class="sub-title">Compare Places</h2>', unsafe_allow_html=True)
    place1 = st.text_input("Enter the first place name", placeholder="e.g., Odisha", key="place1")
    place2 = st.text_input("Enter the second place name", placeholder="e.g., Pune", key="place2")
    criteria_options = ["historical_facts", "attractions", "unique_information"]
    selected_criteria = st.multiselect("Criteria", criteria_options, key="criteria")
    compare_button = st.button("Compare Places")
    
    # Ask user if they want to view the comparison in table form
    display_as_table = st.checkbox("Display results in a table format")

    if compare_button:
        if not place1 or not place2:
            st.error("Both place names are required for comparison.")
        elif not selected_criteria:
            st.error("At least one criterion must be selected for comparison.")
        else:
            places = [place1, place2]
            with st.spinner("Comparing places..."):
                comparison_data = compare_places(places, selected_criteria)

                if comparison_data:
                    if display_as_table:
                        # Extract data for table
                        table_data = []
                        columns = ["Criterion", f"{place1}", f"{place2}"]
                        
                        for criterion in selected_criteria:
                            row = [criterion.capitalize()]
                            for place in [place1, place2]:
                                # Find details for each place and criterion
                                place_data = next((item for item in comparison_data if item["placeName"] == place), {})
                                details = place_data.get("details", {}).get(criterion, "No data available")
                                row.append(details)
                            table_data.append(row)

                        # Display the table
                        st.markdown("### Comparison Results")
                        st.markdown(
                            f"<table style='width:100%; border: 1px solid orange;'>"
                            f"<tr><th>{columns[0]}</th><th>{columns[1]}</th><th>{columns[2]}</th></tr>" +
                            "".join(
                                f"<tr><td>{row[0]}</td><td>{row[1]}</td><td>{row[2]}</td></tr>"
                                for row in table_data
                            ) +
                            "</table>",
                            unsafe_allow_html=True
                        )
                    else:
                        # Display detailed view
                        st.subheader("Comparison Results:")
                        for result in comparison_data:
                            place_name = result.get('placeName', 'Unknown Place')
                            details = result.get('details', {})
                            details_text = ', '.join([f"{k}: {v}" for k, v in details.items()]) or "No details available."
                            st.markdown(f"<div><strong>{place_name}</strong>: {details_text}</div>", unsafe_allow_html=True)
                else:
                    st.warning("No comparison data found.")


# Section for uploading an image
if selected_page == "Upload Image":
    st.subheader("Upload an Image")
    place = st.text_input("Enter the place name for the image:", placeholder="Type Here")
    image_file = st.file_uploader("Choose an image...", type=["jpg", "jpeg", "png"])

    if image_file is not None and place:
        if st.button("Upload Image"):
            with st.spinner("Uploading image..."):
                upload_response = upload_image(image_file, place)
                if upload_response:
                    if upload_response.get("isPlaceImage") == False:
                        st.warning("This image does not match any recognized place in our category.")
                    # st.json(upload_response)
                    else:
                        st.success("Image uploaded successfully!")
                    
    else:
        st.warning("Please enter a place name and select an image to upload.")

# Footer
st.markdown("---")
st.markdown(
    "Made with ❤️ using Streamlit by [Chinmay Jena](https://www.linkedin.com/in/chinmay-jena-0234642a0)",
    unsafe_allow_html=True
)
