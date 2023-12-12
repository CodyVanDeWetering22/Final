import { RepoItem } from "./RepoItem.js";

export class Keep extends RepoItem {
    constructor(data) {
        super(data)

        this.name = data.name
        this.description = data.description
        this.img = data.img
        this.views = data.views
        this.kept = data.kept
        this.creatorId = data.creatorId
        this.creator = data.creator
    }
}


