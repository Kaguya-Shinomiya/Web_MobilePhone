from transformers import MarianMTModel, MarianTokenizer

# Khởi tạo tokenizer và model ở cấp độ module
tokenizer = MarianTokenizer.from_pretrained("Helsinki-NLP/opus-mt-mul-en")
model = MarianMTModel.from_pretrained("Helsinki-NLP/opus-mt-mul-en")

def translate(text):
    tokens = tokenizer(text, return_tensors="pt", padding=True, truncation=True)
    translated = model.generate(**tokens)
    return tokenizer.decode(translated[0], skip_special_tokens=True)

def predict_emotion(sentiment_model, tokenizer, model, text):
    try:
        text_trans = translate(text)
        result = sentiment_model(text_trans)
        return result[0]['label']  # Chỉ trả về 'label' (POSITIVE hoặc NEGATIVE)
    except Exception as e:
        print(f"Lỗi khi dự đoán cảm xúc: {str(e)}")
        return None