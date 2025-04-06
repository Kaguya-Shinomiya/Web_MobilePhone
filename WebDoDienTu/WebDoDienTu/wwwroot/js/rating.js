const stars = document.querySelectorAll('.star-label i');

stars.forEach((star, index) => {
    star.addEventListener('click', () => {
        // Remove active class from all stars
        stars.forEach(s => s.classList.remove('text-warning'));
        // Add active class to all selected stars
        for (let i = 0; i <= index; i++) {
            stars[i].classList.add('text-warning');
        }
        // Set the corresponding radio button as checked
        document.getElementById(`star${index + 1}`).checked = true;
    });
});

document.getElementById("reviewForm").addEventListener("submit", function(event) {
    event.preventDefault();

    // Lấy dữ liệu từ form
    const formData = new FormData(this);

    // Gửi đánh giá qua AJAX
    fetch('/ProductReview/AddReview', {
        method: 'POST',
        body: formData
    })
    .then(response => response.json())
    .then(data => {
        if (data.success) {
            // Hiển thị thông báo thành công với SweetAlert2
            Swal.fire({
                icon: 'success',
                title: 'Success',
                text: data.message,
                confirmButtonText: 'OK'
        }).then(() => {
            // Tải lại trang hiện tại sau khi nhấn OK
            location.reload();
        });
        } else {
            // Hiển thị thông báo lỗi với SweetAlert2
            Swal.fire({
                icon: 'error',
                title: 'Error',
                text: data.message,
                confirmButtonText: 'OK'
            });
        }
    })
    .catch(error => console.error('Error:', error));
});


// Loading rating
let visibleReviews = 5;

function loadMoreReviews() {
    const reviews = document.querySelectorAll('.review-item');
const totalReviews = reviews.length;

for (let i = visibleReviews; i < visibleReviews + 5 && i < totalReviews; i++) {
    reviews[i].style.display = 'block';
    }

visibleReviews += 5;

    // Ẩn nút "Load More" nếu đã hiển thị tất cả đánh giá
    if (visibleReviews >= totalReviews) {
    document.getElementById('loadMoreBtn').style.display = 'none';
    }
}
