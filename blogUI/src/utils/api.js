import axios from 'axios';

axios.defaults.baseURL = 'http://localhost:5000/api';
axios.defaults.headers.common.Authorization = window.localStorage.getItem('accessToken') || '';
axios.defaults.headers.common['Access-Control-Allow-Origin'] = '*';
axios.defaults.headers.post['Content-Type'] = 'application/x-www-form-urlencoded';


module.exports = {
  getUsers() {
    axios.get('/users')
      .then((response) => {
        console.log(response);
      })
      .catch((error) => {
        console.log(error);
      });
  },
};
