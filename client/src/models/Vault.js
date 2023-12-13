export class Vault {
    constructor(data) {
        this.id = data.id
        this.name = data.name
        this.description = data.description
        this.img = data.img
        this.isPrivate = data.isPrivate
        this.creator = data.creator
        this.creatorId = data.creatorId
    }
}




const data = {
    "name": "My Favorite stuff",
    "description": "A Board to save all the things I love",
    "img": "https://images.unsplash.com/photo-1526566762798-8fac9c07aa98?ixlib=rb-1.2.1&ixid=MnwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8&auto=format&fit=crop&w=500&h=400&q=60",
    "isPrivate": true,
    "creatorId": "652eeda69f8ff255572e269a",
    "creator": {
        "name": "cody@gmail.com",
        "picture": "https://s.gravatar.com/avatar/6f414bef4e1193725cc478ff5ae42c98?s=480&r=pg&d=https%3A%2F%2Fcdn.auth0.com%2Favatars%2Fco.png",
        "id": "652eeda69f8ff255572e269a",
        "createdAt": "2023-12-01T18:49:03",
        "updatedAt": "2023-12-01T18:49:03"
    },
    "id": 11,
    "createdAt": "2023-12-12T18:58:40",
    "updatedAt": "2023-12-12T18:58:40"
}