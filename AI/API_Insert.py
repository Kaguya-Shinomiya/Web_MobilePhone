from flask import Flask, request, jsonify
from transformers import pipeline
import API_setEmotion


app = Flask(__name__)
sentiment_model = pipeline("sentiment-analysis", model="distilbert/distilbert-base-uncased-finetuned-sst-2-english", device=0)
model_name = "Helsinki-NLP/opus-mt-mul-en"


@app.route('/insert', methods=['POST'])
def insert_record():
    try:
        data = request.json
        comment = data.get('Comment')
        result = API_setEmotion.predict_emotion(sentiment_model, comment, model_name)
        if result:  
            return jsonify({"message": result}), 201
        else:
            return jsonify({"error": "Can't predict emotion"}), 500
    except Exception as e:
        return jsonify({"error": str(e)}), 500

if __name__ == '__main__':
    app.run(debug=False)