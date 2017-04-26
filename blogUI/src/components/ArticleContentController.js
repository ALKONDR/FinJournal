import React from 'react';
import PropTypes from 'prop-types';
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
    };
    console.log(this.props.match);
  }

  componentDidMount() {
    api.getUserArticle(this.props.match.params.username, this.props.match.params.caption)
      .then((response) => {
        console.log(response);
      });
  }

  render() {
    console.log('I am here');
    return (
      <div>
        {'Here should be article page'}
      </div>
      // <ArticleView articleData={this.state.articleData} />
    );
  }
}

ArticleContentController.propTypes = {
  match: PropTypes.shape({
    params: PropTypes.shape({
      username: PropTypes.string.isRequired,
      caption: PropTypes.string.isRequired,
    }).isRequired,
  }).isRequired,
};

module.exports = ArticleView;
