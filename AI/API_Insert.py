from flask import Flask, request, jsonify
from transformers import pipeline
import API_setEmotion
import logging

# Cấu hình logging
logging.basicConfig(level=logging.INFO)
logger = logging.getLogger(__name__)

app = Flask(__name__)

# Khởi tạo mô hình cảm xúc
sentiment_model = pipeline("sentiment-analysis", model="distilbert/distilbert-base-uncased-finetuned-sst-2-english", device=0)

# Khởi tạo mô hình dịch thuật một lần
model_name = "Helsinki-NLP/opus-mt-mul-en"
tokenizer = API_setEmotion.MarianTokenizer.from_pretrained(model_name)
model = API_setEmotion.MarianMTModel.from_pretrained(model_name)

@app.route('/insert', methods=['POST'])
def insert_record():
    try:
        data = request.json
        comment = data.get('Comment')
        if not comment:
            return jsonify({"error": "Comment is required"}), 400

        logger.info(f"Nhận yêu cầu với comment: {comment}")
        result = API_setEmotion.predict_emotion(sentiment_model, tokenizer, model, comment)
        if result:
            logger.info(f"Phân tích cảm xúc thành công: {result}")
            return jsonify({"message": result}), 201
        else:
            logger.error("Không thể dự đoán cảm xúc")
            return jsonify({"error": "Can't predict emotion"}), 500
    except Exception as e:
        logger.error(f"Lỗi khi xử lý yêu cầu: {str(e)}")
        return jsonify({"error": str(e)}), 500

if __name__ == '__main__':
    app.run(host='0.0.0.0', port=5000, debug=False)