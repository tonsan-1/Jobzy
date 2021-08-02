"use strict";

function onInputChange() {
    document.getElementById('search-input').oninput = function (event) {
        event.preventDefault();

        var queryValue = document.getElementById('search-input').value;

        fetch('https://localhost:44319/Messages', {
            method: 'POST',
            body: JSON.stringify(queryValue),
            headers: {
                'Content-Type': 'application/json'
            }
        })
            .then(res => res.json())
            .then(users => {

                console.log(users);

                if (Object.keys(users).length == 0) {
                    document.querySelector('.dropdown-menu').innerHTML = '';
                    return;
                }

                if (!_.isEqual(previousObject, users)) {
                    document.querySelector('.dropdown-menu').innerHTML = '';

                    Object.keys(users).forEach(key => {
                        console.log(users[key]);

                        document.querySelector('.dropdown-menu')
                            .innerHTML += `<a class="dropdown-item" href="${users[key].id}">${users[key].name}</a>`;
                        document.querySelector('.dropdown-menu')
                            .innerHTML += `<div class="dropdown-divider"></div>`;
                    })

                    var previousObject = Object.assign(users);
                }

                console.log(previousObject);
            });
    }
}

var connection = new signalR.HubConnectionBuilder().withUrl("/MessageHub").build();

//Disable send button until connection is established
document.getElementById("sendButton").disabled = true;

connection.on("ReceiveMessage", function (user, message) {
    //<div class="message-bubble me">
    //    <div class="message-bubble-inner">
    //        <div class="message-avatar"><img src="images/user-avatar-small-01.jpg" alt="" /></div>
    //        <div class="message-text"><p>Thanks for choosing my offer. I will start working on your project tomorrow.</p></div>
    //    </div>
    //    <div class="clearfix"></div>
    //</div>

    console.log(message);
    document.getElementById("message-field").innerHTML +=
        `<div class="message-bubble me">
            <div class="message-bubble-inner">
                <div class="message-avatar"><img src="images/user-avatar-small-01.jpg" alt="" /></div>
                <div class="message-text"><p>${message}</p></div>
        </div>
            <div class="clearfix"></div>
        </div>`
});

connection.start().then(function () {
    document.getElementById("sendButton").disabled = false;
}).catch(function (err) {
    return console.error(err.toString());
});

document.getElementById("sendButton").addEventListener("click", function (event) {
    var username = document.getElementById("username").innerHTML;
    var message = document.getElementById("messageInput").value;
    connection.invoke("SendMessageToUser", username, message).catch(function (err) {
        return console.error(err.toString());
    });
    event.preventDefault();
});