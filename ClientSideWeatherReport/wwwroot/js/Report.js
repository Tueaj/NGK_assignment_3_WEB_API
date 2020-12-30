"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("https://localhost:44316/reporthub").build();

//Disable send button until connection is established
connection.start().catch(function(e) {});

connection.on("SendReport",
    function (report, stationName) {

        var output = "StationName : " + stationName + ", "
        "Date :" +
          report.Time +
          ", " +
          "temp :" +
          report.Temp +
          ", " +
          "Humidity :" +
          report.Humidity +
          ", " +
          "AirPressure :" +
          report.AirPressure +
          ", " +
          "Place of report :" +
          report.Place.Name;


        var li = document.createElement("li");
        li.textContent = output;
        document.getElementById("reportList").appendChild(li);
    });