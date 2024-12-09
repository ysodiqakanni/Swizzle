    const communities = [
        { name: "r/Programming", icon: "https://picsum.photos/200/300", members: 2.5 },
        { name: "r/Technology", icon: "https://picsum.photos/200/300", members: 3.1 },
        { name: "r/Science", icon: "https://picsum.photos/200/300", members: 4.2 },
        { name: "r/Gaming", icon: "https://picsum.photos/200/300", members: 3.7 },
        { name: "r/WorldNews", icon: "https://picsum.photos/200/300", members: 5.1 }
    ];

    const posts = [
    {
        id: 1,
    title: "The Future of AI: Insights and Predictions",
    subreddit: "r/Technology",
    author: "techInsider",
    content: `Artificial Intelligence is rapidly evolving, transforming industries and reshaping our understanding of technology.

    In this comprehensive analysis, we'll explore the emerging trends, potential breakthroughs, and ethical considerations that will define AI's trajectory in the coming years.

    Key Highlights:
    1. Machine Learning Advancements
    2. Ethical AI Development
    3. Industry-Specific Applications
    4. Potential Societal Impacts`,
    comments: [
    {
        id: 1,
    author: "dataScientist",
    content: "Great overview of AI trends!",
    timestamp: "2 hours ago"
                    },
    {
        id: 2,
    author: "techEnthusiast",
    content: "I'm excited about the ethical AI development section.",
    timestamp: "1 hour ago"
                    }
    ],
    comments_count: 42,
    upvotes: 1205,
    timePosted: "5 hours ago",
    tags: ["AI", "Technology", "Future"]
            },
    {
        id: 2,
    title: "AI and Blockchain: A Comprehensive Overview",
    subreddit: "r/Technology",
    author: "blockchainGuru",
    content: `Blockchain technology has revolutionized various industries, including finance, supply chain management, and digital identity. In this comprehensive analysis, we'll explore the core concepts, potential applications, and challenges.`,
    comments: [
    {
        id: 1,
    author: "blocokguru",
    content: "Great overview of Blockchain trends!",
    timestamp: "5 hours ago"
                    },
    {
        id: 2,
    author: "whoami",
    content: "I'm excited about the ethical AI development section.",
    timestamp: "10 hour ago"
                    }
    ],
    comments_count: 2,
    upvotes: 15,
    timePosted: "13 hours ago",
    tags: ["AI", "Blockchain", "Future"]
            },
    {
        id: 3,
    title: "Top 10 Programming Languages in 2024",
    subreddit: "r/Programming",
    author: "codingGuru",
    comments: 203,
    upvotes: 456,
    timePosted: "1 day ago"
            },
    // ... other posts (previous posts can be added here)
    ];

    // Application State
    const AppState = {
        currentView: 'home',
    currentPost: null
        };

    const recentPosts = [
    {title: "Machine Learning Trends", subreddit: "r/Technology" },
    {title: "Climate Change Research", subreddit: "r/Science" },
    {title: "New Game Releases", subreddit: "r/Gaming" }
    ];

    // Render Communities
    function renderCommunities() {
            const communitiesList = document.getElementById('communities-list');
            communities.forEach(community => {
                const communityItem = document.createElement('li');
    communityItem.classList.add('community-item', 'd-flex', 'align-items-center', 'mb-2', 'p-2', 'rounded');
    communityItem.innerHTML = `
    <img src="${community.icon}" alt="${community.name} icon" class="rounded-circle me-3" width="30" height="30">
        <div>
            <strong>${community.name}</strong>
            <div class="text-muted">${community.members}m members</div>
        </div>
        `;
        communitiesList.appendChild(communityItem);
            });
        }

        // Render Main Content
        function renderMainContent() {
            const mainContent = document.getElementById('main-content');

        // Clear previous content
        mainContent.innerHTML = '';

        if (AppState.currentView === 'home') {
                // Render Sidebar and Posts (similar to previous implementation)
                const sidebarCol = document.createElement('div');
        sidebarCol.className = 'col-lg-3 col-md-12 mb-3';
        sidebarCol.innerHTML = `
        <div class="sidebar p-3">
            <h3 class="mb-3">Communities</h3>
            <ul class="list-unstyled" id="communities-list"></ul>
        </div>
        `;

        const postsCol = document.createElement('div');
        postsCol.className = 'col-lg-6 col-md-12 mb-3';
        postsCol.id = 'posts-container';

        const recentPostsCol = document.createElement('div');
        recentPostsCol.className = 'col-lg-3 col-md-12 mb-3';
        recentPostsCol.innerHTML = `
        <div class="recent-posts p-3">
            <h3 class="mb-3">Recent Posts</h3>
            <ul class="list-unstyled" id="recent-posts-list"></ul>
        </div>
        `;

        mainContent.appendChild(sidebarCol);
        mainContent.appendChild(postsCol);
        mainContent.appendChild(recentPostsCol);

        renderCommunities();
        renderPosts();
        renderRecentPosts();
            } else if (AppState.currentView === 'post') {
            renderSinglePostView(AppState.currentPost);
            }
        }

        // Render Single Post View
        function renderSinglePostView(post) {
            const mainContent = document.getElementById('main-content');
        mainContent.innerHTML = `
        <div class="col-12">
            <div class="post-detailed-view">
                <div class="d-flex justify-content-between align-items-center mb-3">
                    <h1 class="h3">${post.title}</h1>
                    <div class="share-dropdown">
                        <button class="btn btn-outline-secondary" id="share-btn">
                            <i class="fas fa-share-alt"></i> Share
                        </button>
                    </div>
                </div>
                <div class="post-meta mb-3 text-muted">
                    Posted in ${post.subreddit} by ${post.author}
                    • ${post.timePosted}
                </div>
                <div class="post-content mb-4">
                    ${post.content.split('\n').map(p => `<p>${p}</p>`).join('')}
                </div>
                <div class="post-actions d-flex align-items-center mb-3">
                    <div class="vote-section me-3">
                        <button class="btn btn-outline-secondary upvote">▲</button>
                        <span class="vote-count mx-2">${post.upvotes}</span>
                        <button class="btn btn-outline-secondary downvote">▼</button>
                    </div>
                    <div class="tags">
                        ${post.tags.map(tag => `<span class="badge bg-secondary me-2">${tag}</span>`).join('')}
                    </div>
                </div>
                <div class="comments-section">
                    <h3 class="mb-3">Comments (${post.comments_count})</h3>
                    ${post.comments.map(comment => `
                                <div class="comment">
                                    <div class="comment-header d-flex justify-content-between mb-2">
                                        <strong>${comment.author}</strong>
                                        <small class="text-muted">${comment.timestamp}</small>
                                    </div>
                                    <p>${comment.content}</p>
                                </div>
                            `).join('')}
                </div>
            </div>
        </div>
        `;

        // Setup share functionality
        const shareBtn = document.getElementById('share-btn');
        const shareModal = new bootstrap.Modal(document.getElementById('shareModal'));

            shareBtn.addEventListener('click', () => {
                const shareLink = window.location.href;
        document.getElementById('share-link').value = shareLink;
        shareModal.show();
            });

            // Copy link functionality
            document.getElementById('copy-link').addEventListener('click', () => {
                const linkInput = document.getElementById('share-link');
        linkInput.select();
        document.execCommand('copy');
        alert('Link copied to clipboard!');
            });

            // Social share functionality
            document.querySelectorAll('.share-platform').forEach(platform => {
            platform.addEventListener('click', (e) => {
                const socialPlatform = e.currentTarget.dataset.platform;
                const shareLink = document.getElementById('share-link').value;

                const socialShareUrls = {
                    twitter: `https://twitter.com/intent/tweet?text=${encodeURIComponent(post.title)}&url=${encodeURIComponent(shareLink)}`,
                    facebook: `https://www.facebook.com/sharer/sharer.php?u=${encodeURIComponent(shareLink)}`,
                    reddit: `https://reddit.com/submit?title=${encodeURIComponent(post.title)}&url=${encodeURIComponent(shareLink)}`
                };

                window.open(socialShareUrls[socialPlatform], '_blank');
            });
            });

        // Voting functionality
        const upvoteBtn = document.querySelector('.upvote');
        const downvoteBtn = document.querySelector('.downvote');
        const voteCount = document.querySelector('.vote-count');

            upvoteBtn.addEventListener('click', () => {
            post.upvotes++;
        voteCount.textContent = post.upvotes;
            });

            downvoteBtn.addEventListener('click', () => {
            post.upvotes--;
        voteCount.textContent = post.upvotes;
            });
        }


        // Render Posts
        // Render Posts (modified to add click event)
        function renderPosts() {
            const postsContainer = document.getElementById('posts-container');
        postsContainer.innerHTML = '';

            posts.forEach(post => {
                const postElement = document.createElement('div');
        postElement.classList.add('post', 'd-flex', 'mb-3', 'p-3');

        postElement.innerHTML = `
        <div class="vote-section me-3">
            <button class="vote-btn upvote">▲</button>
            <span class="vote-count">${post.upvotes}</span>
            <button class="vote-btn downvote">▼</button>
        </div>
        <div class="post-content flex-grow-1">
            <h2 class="post-title h5 mb-2" style="cursor: pointer;">${post.title}</h2>
            <div class="post-meta text-muted">
                Posted in ${post.subreddit} by ${post.author}
                • ${post.timePosted} • ${post.comments_count} comments
            </div>
        </div>
        `;

                // Add click event to open single post view
                postElement.querySelector('.post-title').addEventListener('click', () => {
            AppState.currentView = 'post';
        AppState.currentPost = post;
        renderMainContent();
                });

        const upvoteBtn = postElement.querySelector('.upvote');
        const downvoteBtn = postElement.querySelector('.downvote');
        const voteCount = postElement.querySelector('.vote-count');

                upvoteBtn.addEventListener('click', () => {
            post.upvotes++;
        voteCount.textContent = post.upvotes;
                });

                downvoteBtn.addEventListener('click', () => {
            post.upvotes--;
        voteCount.textContent = post.upvotes;
                });

        postsContainer.appendChild(postElement);
            });
        }

        // Render Recent Posts
        function renderRecentPosts() {
            const recentPostsContainer = document.getElementById('recent-posts-list');
            recentPosts.forEach(post => {
                const postItem = document.createElement('li');
        postItem.classList.add('recent-post-item', 'p-2', 'rounded', 'mb-2');
        postItem.innerHTML = `
        <strong>${post.title}</strong>
        <div class="text-muted">${post.subreddit}</div>
        `;
        recentPostsContainer.appendChild(postItem);
            });
        }

        // Theme Toggle
        function setupThemeToggle() {
            const themeToggle = document.querySelector('.theme-toggle');
        const moonIcon = themeToggle.querySelector('i');

        // Check for saved theme or system preference
        const savedTheme = localStorage.getItem('theme');
        const prefersDarkMode = window.matchMedia('(prefers-color-scheme: dark)').matches;

        if (savedTheme === 'dark' || (!savedTheme && prefersDarkMode)) {
            document.documentElement.setAttribute('data-theme', 'dark');
        moonIcon.classList.replace('fa-moon', 'fa-sun');
            }

            themeToggle.addEventListener('click', () => {
                const isDark = document.documentElement.getAttribute('data-theme') === 'dark';

        if (isDark) {
            document.documentElement.removeAttribute('data-theme');
        moonIcon.classList.replace('fa-sun', 'fa-moon');
        localStorage.setItem('theme', 'light');
                } else {
            document.documentElement.setAttribute('data-theme', 'dark');
        moonIcon.classList.replace('fa-moon', 'fa-sun');
        localStorage.setItem('theme', 'dark');
                }
            });
        }

        // Initialize everything
        document.addEventListener('DOMContentLoaded', () => {
            renderMainContent();
        // renderCommunities();
        // renderPosts();
        // renderRecentPosts();
        setupThemeToggle();
        });
