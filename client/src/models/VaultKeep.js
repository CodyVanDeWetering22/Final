import { Keep } from "./Keep.js";


export class VaultKeep extends Keep {
    constructor(data) {
        super(data);
        this.vaultKeepId = data.id;
        this.vaultId = data.id;
        this.keepId = data.id;
    }
}
