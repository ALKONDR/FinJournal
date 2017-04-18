module.exports = {
  parser: "babel-eslint",
  "rules": {
    "import/no-extraneous-dependencies": [
      "error",
      {
        "devDependencies": true
      }
    ],
    "react/prefer-stateless-function": [
      0,
      {
        
      }
    ]
  },
  "extends": "airbnb",
  "plugins": [
    "react",
    "jsx-a11y",
    "import"
  ],
  "globals": {
    "document": true
  }
};