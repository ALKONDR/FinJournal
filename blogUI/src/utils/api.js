import axios from 'axios';
import qs from 'qs';

axios.defaults.baseURL = 'http://localhost:5000/api';
axios.defaults.headers.common.Authorization = `Bearer ${window.localStorage.getItem('accessToken') || ''}`;
axios.defaults.headers.common['Access-Control-Allow-Origin'] = '*';

module.exports = {
  setDefaults() {
    axios.defaults.baseURL = 'http://localhost:5000/api';
    axios.defaults.headers.common.Authorization = `Bearer ${window.localStorage.getItem('accessToken') || ''}`;
    axios.defaults.headers.common['Access-Control-Allow-Origin'] = '*';
  },

  get userLoggedIn() {
    return !!window.localStorage.getItem('username');
  },

  get loggedInUser() {
    return window.localStorage.getItem('username');
  },

  getUsers() {
    return axios.get('/users');
  },

  login(username, password) {
    this.logout();
    this.setDefaults();
    return axios.post('/login', qs.stringify({ username, password }))
                .then((response) => {
                  if (response.status !== 200) {
                    return false;
                  }

                  window.localStorage.setItem('accessToken', response.data.access.accessToken);
                  window.localStorage.setItem('refreshToken', response.data.refresh);
                  window.localStorage.setItem('username', username);

                  return true;
                });
  },

  logout() {
    window.localStorage.removeItem('accessToken');
    window.localStorage.removeItem('refreshToken');
    window.localStorage.removeItem('username');
  },

  signup(email, username, password) {
    return axios.post('/signup', qs.stringify({ email, username, password }))
                .then((response) => {
                  if (response.status === 200) {
                    return true;
                  }

                  return false;
                });
  },

  refreshToken() {
    return axios.get('/refresh',
      {
        headers: {
          Authorization: `Bearer ${window.localStorage.getItem('refreshToken') || ''}`,
        },
      })
      .then((response) => {
        if (response.status === 200) {
          window.localStorage.setItem('accessToken', response.data.accessToken);
          return true;
        }

        return false;
      });
  },

  getUser(username) {
    return axios.get(`/users/${username}`);
  },

  getUserArticle(username, article) {
    return axios.get(`users/${username}/stories/${article}`);
  },

  addArticle(username, story) {
    this.setDefaults();
    return axios.post(`users/${username}/stories`, story);
  },

  getPopularTopics() {
    const topics = ['popular', 'fintech', 'shares', 'investing'];
    return topics;
  },

  getPopularArticles() {
    const demoPreview = {
      username: 'username',
      date: {
        day: 22,
        month: 'April',
      },
      readingTime: 3,
      caption: 'Some Caption',
      description: 'Description of the article',
      likes: 1488,
      dislikes: 228,
      comments: 42,
    };
    return [demoPreview, demoPreview, demoPreview, demoPreview];
  },

  getSubscriptions() {
    this.setDefaults();
    return axios.get('/users/subscriptions');
  },

  getArticlesByTag(tag) {
    return axios.get(`/tags/${tag}`);
  },

  postComment(username, caption, content) {
    this.setDefaults();
    return axios.post(`/users/${username}/stories/${caption}/comments`, { content });
  },
};
