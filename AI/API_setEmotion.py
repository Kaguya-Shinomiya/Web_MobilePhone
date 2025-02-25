from transformers import MarianMTModel, MarianTokenizer

def translate(text, model_name):
    tokenizer = MarianTokenizer.from_pretrained(model_name)
    model = MarianMTModel.from_pretrained(model_name)

    tokens = tokenizer(text, return_tensors="pt", padding=True, truncation=True)
    translated = model.generate(**tokens)
    return tokenizer.decode(translated[0], skip_special_tokens=True)


def predict_emotion(sentiment_model, text, model_name):
    text_trans = translate(text, model_name)
    result = sentiment_model(text_trans)
    return result[0]['label']

