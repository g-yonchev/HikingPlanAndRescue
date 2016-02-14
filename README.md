# Hiking Plan and Rescue Service

## Todos

- list user\`s tracks
- add file (kml or gpx) for track
- parse track parameters from file
- list top rated tracks and download files
- at user login home add warning for overdue checkout
- suggest other tracks based on current preferences (length, location etc.)
- show statistics for users trainings, total calories burned etc.
- routes can be listed and rated
- cache regression models for user trainings
- automatically add weather conditions for training (some remote service)
- add equipment items for training
- administrator area with CRUD for users, trainings (MVC grid or Kendo UI Grid)
- fix database creation modification times to be UTC
- special html chars handling
- unit testing
- host the app in Azure

## Requirements

- unit testing
- special html chars handling
- host the app in Azure
- 15 controllers
- 40 actions

## Database:

- User
    - Age
    - Weight
    - EquipmentItems
    - TrainingSessions
    - MonitoredTrainingSessions

- Equipment
    - User
    - Name
    - Weight

- Training
    - StartDate
    - EndDate (predict)
    - WaterSupply (predict)
    - CaloriesSupply (predict)
    - EquipmentItems
    - User
    - TrainingSessionWeather (auto or manual fill)
    - Track
    - StressCoeff

- TrainingWeather
    - Training
    - PredominantWindDirection
    - AverageWindStrength
    - AverageTemperature
    - AverageHumidity
    - AverageRainfall

- Track
    - User
    - Length (auto)
    - AverageAscentInclination (auto)
    - Ascent (auto)
    - Descent (auto)
    - StressCoeff (Lenght + AvgAscent / length * 100) (auto)
    - File (kml)
    - Roads(km)
    - Offroads(km)
    - Trails(km)

- TrackVoting
    - User
    - Track
    - Value

- WatchedTraining
    - Training PK,FK
    - DateCheckedIn
    - DateCheckedOut



## Site pages

### PRIVATE

    - Profile
    - Trainings (CRUD)
    - Statistics
    - Monitored Trainings
    - Tracks (CRUD and statistic)
    - Equipment

### PUBLIC

    - MonitoredTrainings
        - Overdue(DEFAULT)
            Shows monitored trainings that are overdue, sorts them by overdue amount
        - Future
            User may decide to join
        - Current
            Shows all checked-in trainings that are currently in progress.
        - Past

    - Tracks (public)
        List and download available Tracks, see users, sort them by difficulty

    - User ranking
        By total distance and time trained

### ADMIN

    - MonitoredTrainings (CRUD)
    - Tracks (CRUD)
    - Users (CRUD)



## Statistics (Track your performance)

Tracker that lets you upload your workouts via .gpx or .kml, set the date and duration and gives statistics about your workout (see from sportstracker app).
You don`t have to use a dedicated app - you can upload any kml or gpx.
Weather is automatically checked from online service and taken into consideration in your statistics or you can manually enter it. wind and direction of wind is also important factor. the kind of terrain as well. You can also enter the amount of water and food consumed and gear used by you. Gear has weight and volume.

## Plan (Plan your next adventure)
You can upload a projected Track for some time in the future (or Tracks for a series of days) and the app will estimate the amount of calories you need (the food required) and the time required and the equipment required to finish the Track based on weather predictions and your performance for previously tracked Tracks. The app takes into consideration all your previous performances for the kind of terrain(off road, ascent and descent), weather(wind direction, temperature, humidity and so on). Maybe some kind of machine learning for predictions (from DSA course the weather prediction algorythm).

## Rescue (Get help from community as early as possible if needed)
The app will also allow users to check in their planned Track and later check it out when done. This way if something happens to them the rescue service will get informaed if they don`t check out in the projected time. If a user abuses the service too many times, he`s banned (doesn`t check out and generates many false signals). The user must provide gsm and gsm of relatives in order first to check if he just forgot to check out.
The app will be open source - meaning every registered user can check warning registered Tracks for other users - the users take care of each other and make sure if someone is lost to contact relatives and rescue services if it turns out to be a real lost case. The user can choose to share their registered Tracks with other users or all. The other users will be notified if he`s not checked out on time. If he registers the Track publicly it will appear in overdue Tracks list where anyone registered can see it.
The end goal is a community that can share their outdoor workout routines and track each others progress and status. That way if something happens to one, others will know as early as possible.