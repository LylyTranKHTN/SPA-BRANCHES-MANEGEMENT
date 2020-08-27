<template>
    <v-container grid-list-xs>
        <v-layout row wrap justify-center>
            <v-flex xs12 sm8 md6>
                <v-card>
                    <v-card-title>
                        <h2> Add new Outlet</h2>
                        <v-btn
                            color="success"
                            outline flat
                            absolute right
                            @click="saveNewRoom()"
                            :disabled="name == ''"
                        >
                            Save
                        </v-btn>
                    </v-card-title>
                    <v-divider></v-divider>
                    <v-form class="px-3 py-2">
                        <!-- name -->
                        <v-text-field
                            v-model="name"
                            label="Service name"
                            required
                            :rules="nameRules"
                        ></v-text-field>
                        <!-- Address -->
                        <v-text-field
                            v-model="address"
                            label="Address"
                            required
                        ></v-text-field>
                         <!-- Email -->
                        <v-text-field
                            v-model="email"
                            label="Email"
                            required
                        ></v-text-field>
                        <!-- Phone -->
                        <v-text-field
                            v-model="phone"
                            label="Phone"
                            required
                        ></v-text-field>
                        <v-layout row wrap align-center>
                            <v-flex>
                                <h3>Images</h3>
                                 <input
                                    type="file"
                                    style="display: none"
                                    ref="fileInput"
                                    accept="image/*"
                                    @change="onFilePicked">
                            </v-flex>
                            <v-flex>
                                  <v-btn
                                    @click="changeAvatar()"
                                    class="right" icon
                                    color="primary">
                                    <v-icon>camera</v-icon>
                                </v-btn>
                            </v-flex>
                        </v-layout>
                        <v-layout row wrap>
                            <v-flex xs3 v-for="image in images" :key="image.index">
                                <!-- <v-chip outline flat label style="height:70px; width: 70px"> -->
                                    <v-avatar size="70px" tile>
                                        <v-img
                                                contain
                                                :src="image"
                                        >
                                            <v-btn
                                                style="height:18px; width: 18px"
                                                small
                                                icon class="error right top"
                                            >
                                                <v-icon small
                                                    style="height:5px; width: 5px"
                                                >close</v-icon>
                                            </v-btn>
                                        </v-img>

                                    </v-avatar>
                                <!-- </v-chip> -->
                            </v-flex>
                        </v-layout>
                       <v-layout row wrap>
                            <!-- enable -->
                            <v-switch class="pt-0"
                                color="primary"
                                label="Enable"
                                v-model="enable"
                            ></v-switch>
                       </v-layout>
                    </v-form>
                </v-card>
            </v-flex>
        </v-layout>
    </v-container>
</template>

<script>
export default {
    data(){
        return{
            imageURL: '',
            images: [],
            name: '',
            address: '',
            email: '',
            phone: '',
            enable: false,
            requireTherapist: false,
            description:'',
            nameRules: [
                v => !!v || 'Name room is required'
            ],
            priceRules: [
                v => !!v || 'Price is required'
            ]
        }
    },
    methods:{
        saveNewRoom(){
            var room = {
                name: this.name,
                enable: this.enable,
                note: this.note
            }
        },
         changeAvatar () {
            this.$refs.fileInput.click()
        },
        onFilePicked (event) {
            const files = event.target.files
            let filename = files[0].name
            if (filename.lastIndexOf('.') <= 0) {
                return alert('Please add a valid file!')
            }
            const fileReader = new FileReader()
                fileReader.addEventListener('load', () => {
                this.images.push(fileReader.result)
            })
            fileReader.readAsDataURL(files[0])
            this.image = files[0]
        }
    }
}
</script>
