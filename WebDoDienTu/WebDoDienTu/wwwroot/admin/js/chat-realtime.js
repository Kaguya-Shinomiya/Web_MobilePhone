const connection = new signalR.HubConnectionBuilder().withUrl("/chatHub").build();
let currentChatUser = null;

connection.start().then(() => {
    console.log("Connected to SignalR.");
    loadChatUsers();
}).catch(err => console.error(err.toString()));

connection.on("ReceiveMessageFromUser", (user, message) => {
    if (currentChatUser === user) {
        const li = document.createElement("li");
        li.className = 'received';
        li.textContent = `${message}`;

        // Thêm phần tử thời gian
        const timeSpan = document.createElement("span");
        timeSpan.className = "message-time";
        timeSpan.textContent = new Date().toLocaleTimeString();
        li.appendChild(timeSpan);

        document.getElementById("messagesList").appendChild(li);
    } else {
        showNotification(`Bạn có tin nhắn mới từ ${user}`);
    }
});

// Gửi tin nhắn từ admin tới user
function sendMessageToUser() {
    const userName = currentChatUser;
    // const userName = document.querySelector('#userChatList .active')?.textContent;
    const message = document.getElementById("messageInput").value;

    if (userName && message) {
        connection.invoke("SendMessageToUser", userName, message)
            .then(() => {
                const li = document.createElement("li");
                li.className = 'sent';
                li.textContent = `${message}`;
                const timeSpan = document.createElement("span");
                timeSpan.className = "message-time";
                timeSpan.textContent = new Date().toLocaleTimeString();
                li.appendChild(timeSpan);
                document.getElementById("messagesList").appendChild(li);
                document.getElementById("messageInput").value = "";
            })
            .catch(err => console.error("Error sending message: " + err.toString()));
    } else {
        console.error("User name and message cannot be empty.");
    }
}

// Hàm chọn cuộc trò chuyện
function selectChat(userName) {
    currentChatUser = userName;
    document.getElementById('chatWithUser').textContent = `Chat with ${userName}`;
    document.getElementById('messagesList').innerHTML = '';

    // Thêm class active cho người dùng đã chọn
    document.querySelectorAll('#userChatList li').forEach(li => {
        li.classList.remove('active');
    });
    document.querySelectorAll('#userChatList li').forEach(li => {
        if (li.textContent.trim() === userName) {
            li.classList.add('active');
        }
    });

    // Tải lịch sử chat với người dùng đã chọn
    connection.invoke("GetChatHistory", userName).then(history => {
        history.forEach(msg => {
            const li = document.createElement("li");
            li.textContent = `${msg.message}`;
            li.className = msg.senderId === "Admin" ? 'sent' : 'received';
            const timeSpan = document.createElement("span");
            timeSpan.className = "message-time";
            timeSpan.textContent = new Date(msg.timestamp).toLocaleTimeString();
            li.appendChild(timeSpan);
            document.getElementById("messagesList").appendChild(li);
        });
    }).catch(err => console.error(err.toString()));
}

// Hàm tải danh sách người dùng đã chat
function loadChatUsers() {
    connection.invoke("GetChatUsers")
        .then(users => {
            const userChatList = document.getElementById("userChatList");
            userChatList.innerHTML = '';
            users.forEach(user => {
                const li = document.createElement("li");
                li.textContent = user.userName;
                li.onclick = () => selectChat(user.userName)
                userChatList.appendChild(li);
            });
        })
        .catch(err => console.error("Error fetching users: " + err.toString()));
}

// Khi nhận tin nhắn mới từ user
connection.on("ReceiveMessageFromUser", (user, message) => {
    showNotification(`Tin nhắn mới từ ${user}`);
});

// Khi có sự kiện thông báo tin nhắn mới
connection.on("NewMessageNotification", () => {
    showNotification("Bạn có tin nhắn mới");
});

// Hàm hiển thị thông báo
function showNotification(message) {
    const notification = document.createElement("div");
    notification.className = "notification";
    notification.textContent = message;
    document.body.appendChild(notification);

    // Tự động ẩn thông báo sau 3 giây
    setTimeout(() => {
        notification.remove();
    }, 3000);
}