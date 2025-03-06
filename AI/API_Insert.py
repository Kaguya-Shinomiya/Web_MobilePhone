from flask import Flask, request, jsonify
import pyodbc
from transformers import pipeline
import API_setEmotion
from datetime import datetime
import locale
locale.setlocale(locale.LC_TIME, "C")

app = Flask(__name__)
sentiment_model = pipeline("sentiment-analysis", device=0)
model_name = "Helsinki-NLP/opus-mt-mul-en"


# Cấu hình kết nối đến MS SQL Server
DB_SERVER = "MSI\SQLEXPRESS"
DB_DATABASE = "QLDoDienTu"


# Chuỗi kết nối
conn_str = f"DRIVER={{SQL Server}};SERVER={DB_SERVER};DATABASE={DB_DATABASE}"

# def insert_into_database(product_id, user_id, your_name, your_email, rating, comment, review_date, is_hidden):
#     try:
#         emotion = API_setEmotion.predict_emotion(sentiment_model, comment, model_name)
        
#         conn = pyodbc.connect(conn_str)
#         cursor = conn.cursor()
        
#          # Chuyển đổi kiểu dữ liệu nếu cần
#         product_id = int(product_id)
#         rating = int(rating)
#         is_hidden = bool(is_hidden)  # Đảm bảo giá trị của IsHidden là 0 hoặc 1
#         #review_date = str(review_date)  # Chuyển datetime thành chuỗi nếu cần
        
#         sql = """
#             INSERT INTO ProductReviews (ProductId, UserId, YourName, YourEmail, Rating, Comment, ReviewDate, IsHidden, Emotion)
#             VALUES (?, ?, ?, ?, ?, ?, ?, ?, ?)
#         """
#         values = (product_id, user_id, your_name, your_email, rating, comment, review_date, is_hidden, emotion)
#         print("SQL Query:", sql)
#         print("Values:", values)
        
        
#         cursor.execute(sql, (product_id, user_id, your_name, your_email, rating, comment, review_date, is_hidden, emotion))
#         conn.commit()
#         conn.close()
#         return True
#     except Exception as e:
#         return str(e)

@app.route('/insert', methods=['POST'])
def insert_record():
    try:
        
        
        data = request.json
        # product_id = data.get('ProductId')
        # user_id = data.get('UserId')
        # your_name = data.get('YourName')
        # your_email = data.get('YourEmail')
        # rating = data.get('Rating')
        comment = data.get('Comment')
        # review_date = datetime.now().strftime("%Y-%m-%d %H:%M:%S")
        # is_hidden = data.get('IsHidden')
        

        data = request.json
        print(comment)
        # Danh sách các trường bắt buộc
        # required_fields = ['ProductId', 'UserId', 'YourName', 'YourEmail', 'Rating', 'Comment', 'IsHidden']

        # # Tìm các trường bị thiếu
        # missing_fields = [field for field in required_fields if field not in data or data[field] is None]

        # if missing_fields:
        #     return jsonify({"error": f"Missing parameters: {', '.join(missing_fields)}"}), 400

        # result = insert_into_database(product_id, user_id, your_name, your_email, rating, comment, review_date, is_hidden)
        result = API_setEmotion.predict_emotion(sentiment_model, comment, model_name)
        print(result)
        if result:  
            return jsonify({"message": result}), 201
        else:
            return jsonify({"error": "Insert failed"}), 500
    except Exception as e:
        return jsonify({"error": str(e)}), 500

if __name__ == '__main__':
    app.run(debug=False)