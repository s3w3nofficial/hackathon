"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/notificationHub").build();
connection.start();

connection.on("DataUpdated", function (type) {
    if (type == "Articles") {
        DotNet.invokeMethodAsync('Web.Client', 'UpdateArticlesStatic')
        //window.location.refresh();
    } else if (type == "Events") {
	    DotNet.invokeMethodAsync('Web.Client', 'UpdateEventsStatic')
    }
});

function LikeArticle(title) {
    connection.invoke("OnArticleLiked", title);
}