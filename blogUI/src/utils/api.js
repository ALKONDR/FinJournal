import axios from 'axios';
import qs from 'qs';

axios.defaults.baseURL = 'http://localhost:5000/api';
axios.defaults.headers.common.Authorization = 'Bearer ' + window.localStorage.getItem('accessToken') || '';
axios.defaults.headers.common['Access-Control-Allow-Origin'] = '*';

module.exports = {
  get userLoggedIn() {
    return window.localStorage.getItem('username') !== null;
  },

  getUsers() {
    return axios.get('/users');
  },

  login(username, password) {
    return axios.post('/login', qs.stringify({ username, password }))
                .then((response) => {
                  if (response.status !== 200) {
                    this.userLoggedIn = false;
                    return false;
                  }

                  window.localStorage.setItem('token', response.data.access.accessToken);
                  window.localStorage.setItem('refreshToken', response.data.refresh);
                  window.localStorage.setItem('username', username);

                  this.userLoggedIn = true;

                  return true;
                });
  },

  logout() {
    window.localStorage.removeItem('token');
    window.localStorage.removeItem('refreshToken');
    window.localStorage.removeItem('username');
  },

  signup(email, username, password) {
    return axios.post('/signup', qs.stringify({ email, username, password }))
                .then((response) => {
                  if (response.status === 200)
                    return true;

                    return false;
                });
  },

  refreshToken() {
    return axios.get('/refresh',
                      {
                        headers: {
                          'Authorization': 'Bearer ' + window.localStorage.getItem('refreshToken') || ''
                        }
                      })
                .then((response) => {
                  if (response.status === 200) {
                    window.localStorage.setItem('token', response.data.accessToken);
                    return true;
                  }

                  return false
                });
  },

  getUser(username) {
    return axios.get(`/users${username}`);
  },

  getUserArticles(username, article) {
    return axios.get(`users/${username}/stories/${article}`);
  },

  addArticle(username, story) {
    return axios.post(`users/${username}/stories`, story);
  },
};
