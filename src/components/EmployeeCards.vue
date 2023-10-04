<template>
  <container fluid>
    <CardDetails :open=open></CardDetails>
          <v-card v-bind:style=cardColour class="grid" max-width="500" width="100%" height="260px" elevation="3" tile v-on:mouseover="showTooltip = true" v-on:mouseout="showTooltip = false"  v-on:click="endreOpen">
                  <div class="tooltip" v-if="showTooltip">
                  Click for more details
                  </div>
            <v-card-title class="test" v-bind:style=bgc> </v-card-title>
            <v-list-item-content>
              <v-list-item-title class="text-h5 mb-1">
                {{ name }}
              </v-list-item-title>
              <v-list-item-subtitle class="mb-2">{{ ansattNr }}</v-list-item-subtitle>
              <v-divider></v-divider>
              <v-list-item-title class="text-h6 mt-2">
                Hours
              </v-list-item-title>
              <v-list-item-subtitle>{{ antallTimerJobbet.toFixed(1) }} </v-list-item-subtitle>
            </v-list-item-content>
  
            <v-avatar class="float-right">
              <v-img src="/man.png"></v-img>
            </v-avatar>
  
            
            <v-card-actions>
             
              <v-btn outlined :dark="true" rounded :color="statusColor">
                Status
              </v-btn>
            </v-card-actions>
          </v-card>
    </container>

  </template>

  <script>
  import CardDetails from './CardDetails.vue';
  export default {
    components: {CardDetails},
    methods: {
      setColor(){
        this.bgc.backgroundColor = this.ansattNrColor;
      },
      setColorStatus(){
        if (this.antallTimerJobbet < this.minTimerIUka || this.antallTimerJobbet > this.maxTimerIUka)
        {
          this.statusColor = "red";
        }
        else
        {
          this.statusColor = "green";
        }
      },

      endreOpen(){
        this.open = !this.open;
      },

      setCardColor(){
        if (this.antallTimerJobbet < this.minTimerIUka || this.antallTimerJobbet > this.maxTimerIUka)
        {
          this.cardColour.backgroundColor = "#FFEBEE"; //red
        }
        else
        {
          this.cardColour.backgroundColor = "#E8F5E9"; //green
        }
      }


    },
    created () {
     this.setColor();
     this.setColorStatus();
     this.setCardColor();
    },

    watch: {
      antallTimerJobbet(){
        this.setColorStatus();
        this.setCardColor();
      }
    },
    props: {
      name: String,
      role: String,
      ansattNr: Number,
      antallTimerJobbet: Number,
      ansattNrColor: String,
      minTimerIUka: Number,
      maxTimerIUka: Number
      
    },
    data() {
      return {
        bgc: {
          backgroundColor: ""
        },
        statusColor: "",
        cardColour: {
          backgroundColor: "",
        },
        showTooltip: false,
        open: true

      };
    },
  };
  </script>

  <style scoped>

  .v-list-item__content {
    display: flex;
    flex-direction: column;
  }
  
  .v-list-item__subtitle {
    color: #757575;
  }
  
  .v-card {
    border-radius: 8px;
  }
  
  .v-card__title,
  .v-card__subtitle {
    font-weight: 500;
    color: #4a4a4a;
  }
  
  .v-divider {
    margin-top: 16px;
    margin-bottom: 16px;
    background-color: #e0e0e0;
  }
  
  .v-btn {
    font-weight: 500;
    text-transform: none;
  }

  .v-card:hover {
    cursor: pointer; 
  transform: scale(1.05);
  }

  .tooltip {
  position: absolute;
  top: -25px;
  left: 50%;
  transform: translateX(-50%);
  background-color: #333;
  color: #fff;
  padding: 5px 10px;
  border-radius: 5px;
  font-size: 14px;
  white-space: nowrap;
}
  </style>
  