# Hiking Plan and Rescue Service




## Todos


### Trainings
- allow users to comment whether they have contacted and warned relatives (maybe mark checkedin training as contacted and then display by whom with comment)
- add how many hours are checked-in trainings overdue
- add user warning for overdue checkin at user homepage
- add option to choose from other tracks or create new one when creating training

### Tracks
- filter tracks by date as well (button for latest or most popular)
- list user\`s tracks
- more detailed information for track listing
- add file (kml or gpx) for track
- parse track parameters from file
- display track in google maps or open street map

### Extras
- suggest other tracks based on current preferences (length, location etc.)
- automatically add weather conditions for training (some remote service)
- add equipment items for training
- unit testing

### Final fixes
- documentation
- public home page with description
- tests







## RESTful services

### Authentication

- __Register__

    POST: http://hikingplanandrescue.azurewebsites.net/api/account/register

    BODY:

        {
            "email":"u1@u.u",
            "password":"u1",
            "confirmPassword":"u1"
        }

- __Get token__

    POST: http://hikingplanandrescue.azurewebsites.net/token
    
    HEADER: Content-Type, VALUE: x-www-form-url-encoded

    BODY:

        grant_type=password&username=u1@u.u&password=u1
    

### Trainings (Authorized)

- __Get All__

    GET: http://hikingplanandrescue.azurewebsites.net/api/Trainings
    
    HEADER: Authorization, VALUE: bearer {access token here!}

- __Get by id__

    GET: http://hikingplanandrescue.azurewebsites.net/api/Trainings/{training id here!}
    
    HEADER: Authorization, VALUE: bearer {access token here!}

- __Edit a training__

    PUT: http://hikingplanandrescue.azurewebsites.net/api/Trainings

    HEADER: Authorization, VALUE: bearer {access token here!}

    BODY:

        {
          "Id": 1073,
          "StartDate": "2016-02-11T16:27:31.687",
          "EndDate": "2016-02-12T03:27:31.687",
          "PredictedEndDate": null,
          "Title": "restedited",
          "Water": 0.9436274401115381,
          "Calories": 2384,
          "Track": {
            "Title": "Track36",
            "Length": 15.266982025393787,
            "Ascent": 699.9512386484776,
            "AscentLength": 7.864620658971658
          }
        } 

- __Add a training__

    POST: http://hikingplanandrescue.azurewebsites.net/api/Trainings

    HEADER: Authorization, VALUE: bearer {access token here!}

    BODY:

        {
          "StartDate": "2016-02-11T16:27:31.687",
          "EndDate": "2016-02-12T03:27:31.687",
          "PredictedEndDate": null,
          "Title": "restTraining1",
          "Water": 0.9436274401115381,
          "Calories": 2384,
          "Track": {
            "Title": "Track36",
            "Length": 15.266982025393787,
            "Ascent": 699.9512386484776,
            "AscentLength": 7.864620658971658
          }
        }

- __Delete a training__

    DELETE: http://hikingplanandrescue.azurewebsites.net/api/Trainings/{training id here!}

    HEADER: Authorization, VALUE: bearer {access token here!}








## Description

### Statistics (Track your performance)

Tracker that lets you upload your workouts via .gpx or .kml, set the date and duration and gives statistics about your workout (see from sportstracker app).
You don`t have to use a dedicated app - you can upload any kml or gpx.
Weather is automatically checked from online service and taken into consideration in your statistics or you can manually enter it. wind and direction of wind is also important factor. the kind of terrain as well. You can also enter the amount of water and food consumed and gear used by you. Gear has weight and volume.

### Plan (Plan your next adventure)
You can upload a projected Track for some time in the future (or Tracks for a series of days) and the app will estimate the amount of calories you need (the food required) and the time required and the equipment required to finish the Track based on weather predictions and your performance for previously tracked Tracks. The app takes into consideration all your previous performances for the kind of terrain(off road, ascent and descent), weather(wind direction, temperature, humidity and so on). Maybe some kind of machine learning for predictions (from DSA course the weather prediction algorythm).

### Rescue (Get help from community as early as possible if needed)
The app will also allow users to check in their planned Track and later check it out when done. This way if something happens to them the rescue service will get informaed if they don`t check out in the projected time. If a user abuses the service too many times, he`s banned (doesn`t check out and generates many false signals). The user must provide gsm and gsm of relatives in order first to check if he just forgot to check out.
The app will be open source - meaning every registered user can check warning registered Tracks for other users - the users take care of each other and make sure if someone is lost to contact relatives and rescue services if it turns out to be a real lost case. The user can choose to share their registered Tracks with other users or all. The other users will be notified if he`s not checked out on time. If he registers the Track publicly it will appear in overdue Tracks list where anyone registered can see it.
The end goal is a community that can share their outdoor workout routines and track each others progress and status. That way if something happens to one, others will know as early as possible.
