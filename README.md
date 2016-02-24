# Hiking Plan and Rescue Service

## Description

This web app allows you to store information about your trainings and later view statistics about your performance. What\`s really cool is that when the app has collected enough data about your performanc on various trainings it can prognose the amount of water, calories and time you will need for your next adventure. To do this a Multivariate Linear Regression algorythm is used. Another cool feature is the ability to check-in your training so that others can see when you are expected to come back and if you happen to be too late, the community can try to contact you, launch a search and rescue mission and contact your relatives. This way you can be a little bit more safer when you wander out alone in the mountain, knowing that someone knows about you and your predicted checkout time. Of course, you shouldn`t be alone in the mountains, but we all know it happens sometimes :).

The web app also features a client mobile app that consumes a WebApi 2 service. See HikingPlanAndRescueMobile in my repo.

Live website: http://hikingplanandrescue.azurewebsites.net


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








## Todos


### Trainings
- add user warning for overdue checkin at user homepage
- add option to choose from other tracks or create new one when creating training

### Tracks
- filter tracks by date as well (button for latest or most popular)
- list user\`s tracks
- more detailed information for track listing
- add file (kml or gpx) for track

### Extras
- parse track parameters from file
- display track in google maps or open street map
- suggest other tracks based on current preferences (length, location etc.)
- automatically add weather conditions for training (some remote service)
- add equipment items for training
- unit testing
