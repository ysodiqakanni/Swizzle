// Mobile Sidebar Toggle
document.getElementById('sidebarToggle').addEventListener('click', function () {
    document.getElementById('sidebar').classList.toggle('show');
});

// Close sidebar when clicking outside
document.addEventListener('click', function (event) {
    const sidebar = document.getElementById('sidebar');
    const sidebarToggle = document.getElementById('sidebarToggle');
    if (!sidebar.contains(event.target) && !sidebarToggle.contains(event.target) && window.innerWidth <= 768) {
        sidebar.classList.remove('show');
    }
});