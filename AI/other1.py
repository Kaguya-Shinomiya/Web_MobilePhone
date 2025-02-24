from transformers import pipeline

sentiment_model = pipeline("sentiment-analysis")

text = "I don't think this product is so good like they say."
result = sentiment_model(text)

print(result)