# from transformers import pipeline

# sentiment_model = pipeline("sentiment-analysis")

# text = "I don't think this product is so good like they say."
# result = sentiment_model(text)

# print(result)


from transformers import MarianMTModel, MarianTokenizer

# Tải model dịch từ nhiều ngôn ngữ sang tiếng Anh
model_name = "Helsinki-NLP/opus-mt-mul-en"
tokenizer = MarianTokenizer.from_pretrained(model_name)
model = MarianMTModel.from_pretrained(model_name)

def translate(text):
    tokens = tokenizer(text, return_tensors="pt", padding=True, truncation=True)
    translated = model.generate(**tokens)
    return tokenizer.decode(translated[0], skip_special_tokens=True)

print(translate("Hola, ¿cómo estás?"))  # Tiếng Tây Ban Nha -> "Hello, how are you?"
print(translate("안녕하세요?"))  # Tiếng Hàn -> "Hello"

