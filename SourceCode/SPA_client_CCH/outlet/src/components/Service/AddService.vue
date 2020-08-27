<template>
    <v-container grid-list-xs>
        <v-layout row wrap justify-center>
            <v-flex xs12 sm8 md6>
                <v-card>
                    <v-card-title>
                        <h2> Add new service</h2>
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
                        <v-layout row wrap justify-center>
                            <v-flex xs1>
                                 <v-btn
                                    @click="changeAvatar()"
                                    icon flat
                                    absolute right
                                    color="success">
                                    <v-icon>edit</v-icon>
                                </v-btn>
                                 <input
                                    type="file"
                                    style="display: none"
                                    ref="fileInput"
                                    accept="image/*"
                                    @change="onFilePicked">
                                <v-avatar
                                    size="60"
                                    color="grey"
                                >

                                    <v-img :src="imageUrl" alt="alt">
                                    </v-img>
                                </v-avatar>
                            </v-flex>
                        </v-layout>
                        <!-- name -->
                        <v-text-field
                            v-model="name"
                            label="Service name"
                            required
                            :rules="nameRules"
                        ></v-text-field>
                        <!-- price -->
                        <v-text-field
                            v-model="price"
                            label="Price"
                            required
                            :rules="priceRules"
                        ></v-text-field>
                        <!-- timeneeded -->
                        <v-select
                            :items="times"
                            v-model="time"
                            label="Time needed"
                        ></v-select>
                        <!-- description -->
                        <v-textarea
                            v-model="description"
                            label="Short description"
                        ></v-textarea>
                       <v-layout row wrap>
                            <!-- enable -->
                        <v-switch class="pt-0"
                            color="primary"
                            label="Enable"
                            v-model="enable"
                        ></v-switch>
                         <!-- Require therapist -->
                        <v-switch class="pt-0"
                            color="primary"
                            label="Require therapist"
                            v-model="requireTherapist"
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
            image: [],
            name: '',
            price: '',
            time: '',
            times:['1', '2', '3'],
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
                this.imageUrl = fileReader.result
            })
            fileReader.readAsDataURL(files[0])
            this.image = files[0]
        }
    }
}
</script>
