/* eslint no-console: ["error", { allow: ["log"] }] */
import axios from 'axios';
import qs from 'qs';

axios.defaults.baseURL = 'http://localhost:5000/api';
axios.defaults.headers.common.Authorization = window.localStorage.getItem('accessToken') || '';
axios.defaults.headers.common['Access-Control-Allow-Origin'] = '*';

module.exports = {
  getUsers() {
    return axios.get('/users');
  },

  login(username, password) {
    axios.post('/login', qs.stringify({ username, password }))
      .then((response) => {
        console.log(response);
        window.localStorage.setItem('token', response.data.access.accessToken);
        window.localStorage.setItem('refreshToken', response.data.refresh);
      })
      .catch((error) => {
        console.log(error);
      });
  },
};
