import React from 'react';
import ArticleView from './ArticleView';
import api from '../utils/api';

class ArticleContentController extends React.Component {
  constructor(props) {
    super(props);

    this.state.articleData = {
      username: '',
      title: '',
      date: {
        day: 0,
        Month: '',
      },
      readingTime: 0,
      caption: '',
      description: '',
      likes: [],
      dislikes: [],
      comments: [],
    }
  }

  componentDidMount() {
    // should get data from server here :D
  }

  render() {
    return (
      <ArticleView articleData={this.state.articleData}/>
    );
  }
}

ArticleContentController.propTypes = {
  match: PropTypes.shape({
    params: PropTypes.shape({
      username: PropTypes.string,
      caption: PropTypes.string,
    }),
  }),
};

module.exports = ArticleView;
