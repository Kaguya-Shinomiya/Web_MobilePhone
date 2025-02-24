const connection = new signalR.HubConnectionBuilder().withUrl("/chatHub").build();

    connection.start().then(() => {
    console.log("Connected to SignalR.");
        loadChatHistory();
    }).catch(err => console.error(err.toString()));

    connection.on("ReceiveMessageFromAdmin", (user, message) => {
        const li = document.createElement("li");
        li.textContent = `${message}`;
        li.classList.add("received");
        const timeSpan = document.createElement("span");
        timeSpan.className = "message-time";
        timeSpan.textContent = new Date().toLocaleTimeString();
        li.appendChild(timeSpan);
        document.getElementById("messagesList").appendChild(li);
        showNotification("Tin nhắn mới");
    });

    // Lắng nghe sự kiện thông báo tin nhắn mới
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

function sendMessage() {
    const messageInput = document.getElementById("messageInput");
    const message = messageInput.value;
    if (message.trim()) {
        connection.invoke("SendMessageToAdmin", message).catch(err => console.error(err.toString()));
    const li = document.createElement("li");
    li.textContent = `${message}`;
    li.classList.add("sent");
    const timeSpan = document.createElement("span");
    timeSpan.className = "message-time";
    timeSpan.textContent = new Date().toLocaleTimeString();
    li.appendChild(timeSpan);
    document.getElementById("messagesList").appendChild(li);
    messageInput.value = "";
        }
    }

function loadChatHistory() {
    connection.invoke("GetChatHistory", "")
        .then(messages => {
            messages.forEach(msg => {
                const li = document.createElement("li");
                li.textContent = msg.message;
                li.classList.add(msg.senderId === "Admin" ? "received" : "sent");
                const timeSpan = document.createElement("span");
                timeSpan.className = "message-time";
                timeSpan.textContent = new Date(msg.timestamp).toLocaleTimeString();
                li.appendChild(timeSpan);
                document.getElementById("messagesList").appendChild(li);
            });
        })
        .catch(err => console.error("Error loading chat history: " + err.toString()));
    }

function toggleChat() {
        const chatWindow = document.getElementById("chatWindow");
chatWindow.style.display = chatWindow.style.display === "none" || chatWindow.style.display === "" ? "flex" : "none";
    }