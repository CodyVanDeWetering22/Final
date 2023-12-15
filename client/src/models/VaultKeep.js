import { Keep } from "./Keep.js";


export class VaultKeep extends Keep {
    constructor(data) {
        super(data);
        this.vaultKeepId = data.vaultKeepId;
        this.vaultId = data.vaultId;
        this.keepId = data.keepId;
    }
}
