function toggleChatBot() {
    let chatContainer = document.getElementById("chatBotContainer");
    chatContainer.style.display = chatContainer.style.display === "none" ? "block" : "none";
}

function sendMessage(event) {
    if (event.key === "Enter") {
        let message = document.getElementById("chatInput").value;
        if (message.trim()) {
            addMessage("Bạn", message);
            fetch("/ChatBot/SendMessage", {
                method: "POST",
                headers: {
                    "Content-Type": "application/json"
                },
                body: JSON.stringify({ userMessage: message })
            })
                .then(response => response.json())
                .then(data => addMessage("Bot", data.response))
                .catch(error => console.error(error));
        }
        document.getElementById("chatInput").value = "";
    }
}

function addMessage(sender, text) {
    let chatBody = document.getElementById("chatBotBody");
    let messageDiv = document.createElement("div");
    messageDiv.className = sender === "Bạn" ? "userMessage" : "botMessage";
    messageDiv.innerHTML = `<strong>${sender}: </strong>${text}`;
    chatBody.appendChild(messageDiv);
    chatBody.scrollTop = chatBody.scrollHeight;
}
