
---

# GroqPlaceInfoApi

This API provides various endpoints to retrieve details about specific places, such as historical facts, attractions, famous places, and unique information. Additionally, it includes functionality to verify and upload images if they represent a specific place.

The API is built using ASP.NET Core, with two main controllers:
1. **PlaceInfoController**: Manages retrieval of place-related data.
2. **ImageRecognitionController**: Handles image verification and recognition for places.

## Features

- **Place Information Retrieval**: Provides endpoints to get comprehensive details or specific information about a place.
- **Image Recognition**: Allows users to upload images, which are verified to see if they match a specific place before uploading.

---

## Table of Contents

- [Endpoints](#endpoints)
- [Getting Started](#getting-started)
- [Examples](#examples)

---

## Endpoints

### PlaceInfoController

1. **Get Place Details**
   - **URL**: `GET /api/details/{place}`
   - **Description**: Fetches all details(Historical,Attractions,Famous Places,Unique Information) for a specific place.
   - **Response**: JSON with place details.
   - **Output** : {
    "requestType": "Place Info",
    "placeName": "Pune",
    "details": {
        "historical_facts": "Pune, the cultural hub of Maharashtra! Here are 5 points on its historical significance:\n\n1. **Maratha Empire's Capital**: Pune was the capital of the Maratha Empire from 1720 to 1818. The city was an important center of power, politics, and culture during the Maratha era, and many historical landmarks like the Shani Shingnapur temple and the Kasba Ganpati temple date back to this period.\n\n2. **Freedom Movement**: Pune was a significant center of the Indian freedom movement. Mahatma Gandhi, Lokmanya Tilak, and other leaders played a crucial role in the Indian independence struggle. The city was home to the Kesari, a Marathi newspaper that was instrumental in spreading awareness about the freedom movement.\n\n3. **Education and Intellectual Hub**: Pune has a rich educational history, with institutions like the University of Pune (established in 1948) and the Fergusson College (established in 1880). The city was also home to many notable intellectuals like the Indian critic and politician, Mahadev Govind Ranade.\n\n4. **War Heritage**: Pune played a significant role in World War I and II. The city's military cantonments, like the Poona Horse and the Queen's Own Guides Cavalry, were important centers of military activity. The city's airbase, which was established in 1939, was a key base for the Royal Air Force.\n\n5. ** Cultural and Literary Heritage**: Pune has been an important center of culture and literature for centuries. The city is home to the famous Mahatma Phule Museum, which houses a collection of artifacts related to education, science, and literature. The city's literary heritage is also reflected in the works of famous authors like Shri Krishna Prasad, Vijaya Pandit, and Vinda Karandikar.\n\nThese are just a few examples of Pune's rich cultural and historical heritage. The city has much to offer, from its historic landmarks to its educational institutions and cultural events.",
        "attractions": "Pune! A beautiful city in the Indian state of Maharashtra. Here are 5 main attractions in Pune:\n\n1. **Agardande Hill**: A popular hilltop destination offering a panoramic view of the city. It's a great spot for trekking, picnics, and sunset views.\n2. **Shani Shingnapur**: A sacred town known for its unique Ganesh Temple, where the idol is believed to have been self-installed. It's a popular pilgrimage site and a great place to learn about Maratha history and culture.\n3. **Osho International Meditation Resort**: A peaceful retreat built on an 32-acre campus, featuring lush gardens, meditation rooms, and yoga facilities. Attend meditation sessions, workshops, and conferences to rejuvenate your mind and body.\n4. **Pataleshwar Cave Temple**: A 8th-century Hindu temple carved out of a single rock, dedicated to Lord Shiva. The temple's intricate carvings and peaceful atmosphere make it a must-visit attraction.\n5. **Sinhagad Fort**: A historic fort situated about 30 km from Pune, offering stunning views of the surrounding landscape. Take a trek to the top for a thrilling experience and enjoy the sunset from its ramparts.\n\nThese are just a few of the many attractions Pune has to offer. Enjoy your visit!",
        "famous_places": "Pune! A city steeped in history, culture, and natural beauty. Here are 5 famous places you shouldn't miss in Pune:\n\n1. **Shani Shingnapur**: A religious site around 70 km from Pune, known for its unique temple where the walls are devoid of doors and windows, yet the idol of Lord Shani is said to protect the valuables left within.\n2. **Lal Mahal**: A beautiful palace built in the 17th century for Shivaji Maharaj, the founder of the Maratha Empire. It's now a popular tourist attraction and a great spot for history enthusiasts.\n3. **Koregaon Park**: A trendy neighborhood with a mix of old and new Pune. This area is famous for its restaurants, bars, and shopping centers. Don't miss the beautiful Koregaon Park Lake and the ancient temple dedicated to Goddess Bhavani.\n4. **Osho Teerth Park**: A peaceful oasis in the heart of the city, dedicated to the spiritual guru Osho. The park features beautiful gardens, a meditation center, and a museum showcasing Osho's life and teachings.\n5. **Rajiv Gandhi Zoological Park**: A must-visit for animal lovers! Spread over 130 acres, the zoo is home to over 140 species of animals, including the African lion, white tiger, and Indian rhino. The park also features a garden and a lake.\n\nThese places offer a great mix of culture, history, and natural beauty, giving you a well-rounded experience of Pune's essence.",
        "unique_information": "Pune! Here are 5 unique facts about this fascinating city:\n\n1. **Oxford of the East**: Pune is often referred to as the \"Oxford of the East\" due to the presence of several prestigious educational institutions, including the University of Pune, which is one of the largest and most reputed universities in India.\n2. **Hill Stations**: Pune is situated near 5 hill stations, including Lonavala, Khandala, Mahabaleshwar, Panchgani, and Kamshet. These hill stations offer a peaceful escape from the city's hustle and bustle, with stunning views of the Western Ghats.\n3. **Chandrappa Waterfalls**: Pune is home to the enchanting Chandrappa Waterfalls, located in the nearby town of Hadapsar. The waterfalls are named after a local saint, Chandrappa, who is said to have meditated at the site. Visitors can take a dip in the water or simply relax and enjoy the serene surroundings.\n4. **Pune-Tikona Hill**: Pune has its own hill, known as Tikona Hill, which is a popular trekking spot. The hill offers breathtaking views of the surrounding landscape and is said to have a ancient fort, built by the Pandavas from Hindu mythology.\n5. **Food Capital**: Pune is a foodie's paradise, with a mix of traditional Maharashtrian cuisine, street food, and international dining options. Some popular local dishes include Misal Pav, Pani Puri, and Vada Pav. The city is also famous for its variety of fresh fruits and vegetables, which are widely available at local markets.\n\nThese are just a few of the many fascinating aspects of Pune. There's much more to explore and experience in this vibrant and eclectic city!"
    }
}
   

2. **Get Historical Facts**
   - **URL**: `GET /api/historical_facts/{place}`
   - **Description**: Fetches historical facts for a specific place.
   - **Response**: JSON with historical facts.
   - **Output**: {
    "placeName": "Pune",
    "requestType": "historical_facts",
    "historical_facts": "Pune, the cultural hub of Maharashtra! Here are 5 historical points that highlight its significance:\n\n1. **Maratha Empire**: Pune was the capital of the Maratha Empire under the rule of Shivaji and his successors. The city was a strategic location for the Marathas, allowing them to control the trade and commerce routes between Central India and the Western Coast.\n2. **Chhatrapati Shahu Maharaj**: In 1728, Chhatrapati Shahu Maharaj shifted the Maratha capital from Rajgad to Pune, which became a significant turning point in the city's history. Today, the Shani Shingnapur Temple, which houses the statue of Shahu Maharaj, is a major landmark in Pune.\n3. **Peshwas and the British**: In 1774, the Peshwas (advisors to the Maratha Empire) made Pune their capital. The city remained a key location for the Peshwas during the Maratha-British wars. In 1817, the British took control of Pune, marking the end of the Maratha Empire.\n4. **Indian Independence Movement**: Pune played a crucial role in the Indian Independence Movement, with notable freedom fighters like Bal Gangadhar Tilak, Mahatma Gandhi, and Sardar Patel visiting and addressing rallies in the city. The Aga Khan Palace, where Mahatma Gandhi was imprisoned during the Quit India Movement, is now a museum and a popular tourist attraction.\n5. **Educational and Cultural Hub**: Pune has emerged as a major educational hub, with numerous prestigious institutions like the University of Pune (now Savitribai Phule Pune University), Fergusson College, and National Institute of Virology. The city is also renowned for its cultural heritage, with festivals like Ganesh Chaturthi and Lohri being celebrated with great enthusiasm.\n\nThese points provide a glimpse into Pune's rich history, which spans over 300 years. The city's strategic location, cultural significance, and contributions to Indian history make it an attractive destination for tourists and historians alike."
}
3. **Get Attractions**
   - **URL**: `GET /api/attractions/{place}`
   - **Description**: Retrieves popular attractions for the specified place.
   - **Response**: JSON with attractions list.
   - **Output** : {
    "placeName": "Pune",
    "requestType": "attractions",
    "attractions": "Pune is a vibrant city in Western Maharashtra, India, known for its rich history, cultural heritage, and natural beauty. Here are five main attractions in Pune:\n\n1. **Agri Tourism Farm**: A unique concept that allows visitors to experience rural life, organic farming, and sustainable living. You can participate in farming activities, animal care, and enjoy a farmhouse breakfast.\n\n2. **Osho International Meditation Resort**: A spiritual hub that attracts visitors from around the world. The resort offers meditation, yoga, and other wellness programs, set amidst beautiful gardens and tranquil environments.\n\n3. **Agakhan Palace**: A historic monument built in the 18th century, serving as a museum showcasing the Mughal and Indo-Islamic architectural styles. The palace was also the scene of the Aga Khan's imprisonment during the India-Pakistan Partition.\n\n4. **Sinhagad Fort**: A popular historical fort, famous for the battle fought between the Maratha king Shivaji and the Bijapur Sultan in 1671. The fort offers stunning views of the city and is a great spot for trekking and picnicking.\n\n5. **Pavana Lake**: A serene and picturesque lake, offering boating facilities and scenic views. You can also take a peaceful walk around the lake or a boat ride to enjoy the beauty of the surrounding hills.\n\nThese are just a few of the many attractions Pune has to offer. Depending on your interests, there are plenty of other options to explore in and around the city!"
}

4. **Get Famous Places**
   - **URL**: `GET /api/famous_places/{place}`
   - **Description**: Retrieves a list of famous places in the specified area.
   - **Response**: JSON with famous places list.
   - **Output** : {
    "placeName": "Pune",
    "requestType": "famous_places",
    "famous_places": "Pune, the cultural hub of Maharashtra, has a lot to offer. Here are 5 famous places you shouldn't miss:\n\n1. **Agakhan Palace**: A historic palace that served as a prison to Mahatma Gandhi and other Indian freedom fighters. It's a beautiful blend of Mediterranean and Indian architectural styles.\n\n2. **Shani Shingnapur temple**: A famous pilgrimage spot located near Pune, known for its unique phenomenon where the main deity has no doors or locks, and yet, the temple remains intact and protected.\n\n3. **Pune Darshan Observation Point**: A scenic spot with a stunning view of the city, offering a panoramic view of the Sahyadri mountains and the city skyline. It's a great place to watch the sunset.\n\n4. **Sinhagad Fort**: A historic fort situated near Pune, which played a crucial role in the Indian epic, the Mahabharata. It offers breathtaking views of the surrounding countryside and is a popular trekking spot.\n\n5. **Osho Ashram**: A peaceful oasis in the heart of the city, dedicated to the teachings of the spiritual leader Osho. You can explore the beautiful gardens, take part in meditation sessions, and even stay in one of the many guesthouses.\n\nThese should give you a good taste of Pune's history, culture, and natural beauty!"
}

5. **Get Unique Information**
   - **URL**: `GET /api/unique_information/{place}`
   - **Description**: Fetches unique or notable information about a place.
   - **Response**: JSON with unique information.
   - **Output**: {
    "placeName": "Pune",
    "requestType": "unique_information",
    "unique_information": "Pune! Here are 5 unique points about this wonderful city:\n\n1. **India's Oxford**: Pune is often referred to as the \"Oxford of the East\" due to its numerous prestigious educational institutions, including the University of Pune, Savitribai Phule Pune University, and the Indian Institute of Science Education and Research. These institutions attract students from all over India and abroad.\n\n2. **Birthplace of Cricket in India**: In 1886, the first cricket match was played at the Deccan Gymkhana Ground in Pune. This historic ground has hosted several international matches and is considered the birthplace of cricket in India.\n\n3. **Home to the Maratha Empire's Heritage**: Pune was the former capital of the Maratha Empire, and the city still boasts several historical landmarks like the Shani Shingnapur temple, the Shatrumati temple, and the Aga Khan Palace, which was a key location during the Quit India Movement led by Mahatma Gandhi.\n\n4. **Pune's Vibrant Nightlife**: Contrary to its reputation as a traditional city, Pune has a thriving nightlife scene! The city has many bars, clubs, and lounges, especially in areas like Koregaon Park, Kalyani Nagar, and Boat Club Road. The famous Kalyani Nagar street is often compared to India's version of Amsterdam's famous Leidsestraat.\n\n5. **Pune's Organic Farming and Local Markets**: Pune is known for its vibrant local markets, such as the famous Phule Market, which offers a variety of fresh produce, vegetables, and fruits. The city also has a strong focus on organic farming, with many farmers' markets and community-supported agriculture (CSA) programs available. You can find these markets and programs by searching for \"Organic Farming in Pune\" or \"Farmers Markets in Pune\"."
}

6. **Get Trivia and Fun Facts**
   - **URL**: `GET /api/trivia/{place}`
   - **Description**: Retrieves trivia and fun facts about a specified place.
   - **Response**: JSON with trivia details.
   - **Output**: {
    "placeName": "Pune",
    "requestType": "Trivia and Fun Facts",
    "trivia": "Pune! The Queen of Deccan, a city steeped in history, culture, and natural beauty. Here are some fun and interesting trivia facts about Pune:\n\n1. **The Oxford of the East**: Pune is often called the Oxford of the East due to the high concentration of educational institutions in the city. Many of India's top colleges and universities, including the Indian Institute of Science Education and Research (IISER), Indian Institute of Technology (IIT), and Savitribai Phule Pune University, are located in Pune.\n2. **The Birthplace of the Indian IT Industry**: Pune is often credited as the birthplace of India's IT industry. The city was home to TCS (Tata Consultancy Services), which was founded in 1968 as the first Indian software company.\n3. **Adivasi and Birsa Munda's Connection**: Pune has a significant connection to the Adivasi (indigenous) community of India. The city is home to the Samarth Ramdas Swami Temple, which is associated with Birsa Munda, a legendary Adivasi leader who fought against British rule.\n4. **The oldest surviving synagogue in India**: The Benzia Synagogue, built in 1730, is the oldest surviving synagogue in India and is located in the Lakadiyal neighborhood of Pune.\n5. **Pune is home to the largest water park in Asia**: Adlabs Imagica, located on the outskirts of Pune, is one of the largest and most popular water parks in Asia, covering an area of over 130 acres.\n6. **The city has a rich Maratha heritage**: Pune is steeped in Maratha history, with many historical landmarks, including the Sinhagad Fort, Parvati Hill, and the Deccan Plateau, which offer stunning views of the city.\n7. **Pune is a hub for motorcycle manufacturers**: Pune is home to several prominent motorcycle manufacturers, including Bajaj Auto, which is one of the largest motorcycle manufacturers in the world.\n8. **The city has a vibrant food scene**: Pune is known for its rich and diverse culinary scene, with popular street food like Misal Pav, Poha, and Vada Pav. The city is also home to many excellent restaurants serving traditional Indian, Italian, and Chinese cuisine.\n9. **Pune has a strong connection to the Indian independence movement**: Mahatma Gandhi and Subhas Chandra Bose were both associated with Pune, with Gandhi spending time in the city during his tour of India in 1915-1916 and Bose hiding in the city during his Rebellion against British rule in 1942.\n10. **The city has a moderate climate**: Pune has a mild climate, with temperatures ranging from 12°C to 37°C (54°F to 98.6°F) throughout the year, making it an attractive destination for tourists and residents alike.\n\nThese are just a few of the many interesting facts about Pune. The city has a rich history, vibrant culture, and natural beauty, making it a fascinating place to explore and learn about.",
    "fun_facts": "Pune! This vibrant city in western India has many unexpected facets, just waiting to be discovered. As your knowledgeable assistant, I'm delighted to share some unique and quirky facts about Pune that might surprise even locals:\n\n1. **The Oxford of the East**: Pune is often referred to as the Oxford of the East due to its affiliations with education. It's home to several esteemed institutions, including the Indian Institute of Science Education and Research (IISER), Symbiosis International University, and the National Defence Academy.\n2. **The City of Festivals**: Pune is a celebration-lover's paradise. The city hosts numerous festivals throughout the year, including the renowned Purandar Festival, which commemorates the founder of the Maratha Empire, Shivaji Maharaj. Don't miss the Vagdevi Festival, which honors the goddess of music and the arts!\n3. **The Birthplace of Hindustani Music**: The city's connection to Indian classical music is undeniable. Pune was the birthplace of Hindustani music, which is a blend of Indian and Persian musical styles. Visit the Indian Music Museum to learn more about this rich cultural heritage.\n4. **A Botanist's Paradise**: The city is home to the Jawaharlal Nehru Botanical Garden, which is accredited by the Botanical Gardens Conservation International (BGCI). This 90-acre oasis features over 1,000 species of plants, including rare and endangered ones.\n5. **Pune's Secret Beach**: Who knew Pune had a beach?! The city's own Hadapsar lake has a secret beach area, perfect for a relaxing afternoon with friends or a romantic sunset. Just look for the small jetty near the lake's eastern edge.\n6. **The City's Quirky Traffic Rules**: Pune's traffic rules are... interesting. There's no right-of-way at roundabouts, and you might need to navigate some unconventional driving styles. However, the city's residents have adapted, and you'll find innovative ways to tackle the roads.\n7. **The Royal Connection**: The city is dotted with royal residences, including the spectacular Shani Shingnapur Fort, which dates back to the 16th century. This UNESCO World Heritage Site is said to be impervious to natural disasters and theft!\n8. **Pune's Urban Forest**: The city boasts an impressive urban forest, spanning over 250 hectares. The Rajiv Gandhi Zoological Park and the Sambhaji Park are must-visit spots to connect with nature in the heart of the city.\n9. **The Birthplace of Formula 1 Racing in India**: The city hosted the first-ever Formula 1 racing event in India in 2011. You can relive the excitement at the MMRT Racing Track, which is an integral part of the city's racing heritage.\n10. **The Street Food Heaven**: Pune is a foodie's paradise, with its delectable street food scene. Be sure to try the famous misal pav, pani puri, and bun maska at local eateries like the iconic Bavdhan Chowk or the bustling streets of Sadhu Vaswani Chowk.\n\nThese quirky facts should give you a better sense of what makes Pune such an interesting and vibrant city. Whether you're a local or just visiting, there's always something new to discover in this fabulous city!"
}.

7. **Compare Places**
   - **URL**: `POST /api/compare`
   - **Description**: Compares specified places based on criteria.
   - **Request Body**:
     ```json
     {
       "places": ["Place1", "Place2"],
       "criteria": ["Criteria1", "Criteria2"]
     }
     ```
   - **Response**: [
    {
        "placeName": "BALASORE",
        "details": {
            "famous_places": "Balasore is a city in Odisha, India! Here are 5 famous places to visit in Balasore:\n\n1. Panchalingeswar: A beautiful waterfall located in the Banki River, surrounded by lush green forests. It's about 42 km from Balasore city.\n2. Chandipur Beach: A 12-km stretch of beach famous for its receding tides. It's a perfect spot for swimming, surfing, and sunbathing.\n3. Tara Tarini Temple: A famous Shaktipeetha shrine dedicated to Goddess Tara and Tarini. It's situated on the banks of the Budhabalanga River.\n4. Shyamtapur Beach: A peaceful and picturesque beach with calm waters, ideal for sunset views and relaxing picnics.\n5. Baliapanda Beach: A busy beach with a pier and a bustling market. It's a popular spot for shopping and trying local street food.\n\nThese are just a few of the many amazing places to visit in Balasore. The city has a rich cultural heritage and natural beauty, making it a great destination for tourists and adventure seekers!"
        }
    },
    {
        "placeName": "BARIPADA",
        "details": {
            "famous_places": "Baripada! The largest city in the Mayurbhanj district of Odisha, India. Here are 5 famous places to visit in and around Baripada:\n\n1. Simlipal National Park: A UNESCO World Heritage Site, this national park is located approximately 110 km from Baripada. It's a treasure trove of wildlife and natural beauty, home to tigers, leopards, elephants, and many more species.\n2. Bankadevi Temple: A popular Hindu temple dedicated to Goddess Banka Devi, this temple is situated in the heart of Baripada city. The temple is famous for its ornate architecture and stunning views of the surrounding areas.\n3. Tikarpada Wildlife Sanctuary: Also known as Satkosia Gorge, this wildlife sanctuary is a mere 45 km from Baripada. It's known for its stunning landscapes, waterfalls, and diverse wildlife, including Gangetic dolphins and tigers.\n4. Karlapat Wildlife Sanctuary: Another nearby wildlife sanctuary, Karlapat is about 70 km from Baripada. It's a haven for nature lovers, with lush forests, waterfalls, and a wide variety of flora and fauna.\n5. Baripada Folklore Museum: This museum is dedicated to preserving the rich cultural heritage of Odisha, particularly the Mayurbhanj region. It features an impressive collection of artifacts, exhibits, and exhibits showcasing the region's history, art, and culture.\n\nThese are just a few of the many attractions Baripada has to offer. The city and its surroundings are steeped in history and natural beauty, making it an exciting destination for travelers!"
        }
    }
].

### ImageRecognitionController

1. **Get Place Images**
   - **URL**: `GET /api/getPlaceImages/{placeName}`
   - **Description**: Retrieves images associated with a place.
   - **Response**: JSON with image recognition results.

2. **Recognize and Upload Image**
   - **URL**: `POST /api/recognize`
   - **Description**: Uploads an image only if it matches a specified place.
   - **Form Parameters**:
     - `file`: The image file to upload.
     - `placeName`: The name of the place to match.
   - **Response**: JSON indicating whether the image was recognized as matching the place, along with any detected labels and the S3 URL if uploaded.

---

### Running the Application

1. Clone the repository.
   ```bash
   git clone https://github.com/ChinmayKuJena/tourBackend-DotNet.git
   ```
2. Navigate to the project directory.
   ```bash
   cd GroqPlaceInfoApi
   ```
3. Build the project.
   ```bash
   dotnet build
   ```
4. Run the application.
   ```bash
   dotnet run
   ```

The API should now be running locally at `http://localhost:5000/api/`.

---

## Examples

### Example Requests

1. **Fetch All Place Details**  
   ```bash
   curl -X GET "http://localhost:5000/api/details/Paris"
   ```

2. **Fetch Historical Facts**  
   ```bash
   curl -X GET "http://localhost:5000/api/historical_facts/London"
   ```

3. **Upload and Verify Place Image**  
   ```bash
   curl -X POST "http://localhost:5000/api/recognize" -F "file=@path/to/your/image.jpg" -F "placeName=Eiffel Tower"
   ```

### Sample JSON Responses

- **Place Details**:
  ```json
  {
    "name": "Paris",
    "country": "France",
    "population": 2148000,
    "attractions": ["Eiffel Tower", "Louvre Museum"],
    "historical_facts": ["Paris was founded in the 3rd century BC."],
    "unique_information": "Known as the city of lights."
  }
  ```

- **Image Recognition Result**:
  ```json
  {
    "isPlaceImage": true,
    "detectedLabels": ["Eiffel Tower", "Paris", "Landmark"],
    "url": "https://s3.amazonaws.com/bucket-name/path/to/image.jpg"
  }
  ```

---

## Notes

- The service utilizes **GroqService** for obtaining place details and verifying valid place names.
- **ImageRecognitionService** processes images to verify if they match a particular place.
- In future iterations, Kafka support may be added for producing messages upon successful API actions.

---

## License

This project is licensed under the MIT License.

---
