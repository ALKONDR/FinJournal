import React from 'react';
import PropTypes from 'prop-types';
import ArticleView from './ArticleView';
import api from '../utils/api';

class ArticleContentController extends React.Component {
  constructor(props) {
    super(props);

    this.state = {
      articleData: {
        username: '',
        caption: '',
        content: '',
        date: {
          day: '',
          month: '',
        },
        readingTime: 0,
        description: '',
        likes: [],
        dislikes: [],
        comments: [],
      },
    };

    this.prepareDataForView = this.prepareDataForView.bind(this);
    this.onUpdate = this.onUpdate.bind(this);
  }

  componentWillMount() {
    api.getUserArticle(this.props.match.params.username, this.props.match.params.caption)
      .then((response) => {
        if (response.status >= 200 && response.status < 300) {
          this.prepareDataForView(response.data);
        }
      });
  }

  onUpdate(key, data) {
    switch (key) {
      case 'comments':
        this.setState({ comments: data });
        break;
      case 'likes':
        this.setState({ likes: data });
        break;
      case 'dislikes':
        this.setState({ dislikes: data });
        break;
      default:
        break;
    }
  }

  prepareDataForView(data) {
    this.setState({
      articleData: {
        username: data.author,
        caption: data.title,
        content: data.content,
        date: {
          day: Date(data.date).split(' ')[2],
          month: Date(data.date).split(' ')[1],
        },
        readingTime: data.readingTime,
        description: data.description || 'No description provided',
        likes: data.likes,
        dislikes: data.dislikes,
        comments: data.comments,
      },
    });
  }

  render() {
    return (
      <ArticleView articleData={this.state.articleData} onUpdate={this.onUpdate} />
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

module.exports = ArticleContentController;
