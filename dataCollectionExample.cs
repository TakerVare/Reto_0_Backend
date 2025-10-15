namespace Reto_0_Backend
{
    using System.Collections.Generic;
    using Reto_0_Backend.Models;

    public class DataCollectionExample
    {
        public List<Category> CategoryCollectionList { get; set; }

        public DataCollectionExample()
        {
            CategoryCollectionList = new List<Category>
            {
                new Category(
                    "drought",
                    "Drought",
                    "https://eonet.gsfc.nasa.gov/api/v3/categories/drought",
                    "Long lasting absence of precipitation affecting agriculture and livestock, and the overall availability of food and water.",
                    "https://eonet.gsfc.nasa.gov/api/v3/layers/drought"
                ),
                new Category(
                    "dustHaze",
                    "Dust and Haze",
                    "https://eonet.gsfc.nasa.gov/api/v3/categories/dustHaze",
                    "Related to dust storms, air pollution and other non-volcanic aerosols. Volcano-related plumes shall be included with the originating eruption event.",
                    "https://eonet.gsfc.nasa.gov/api/v3/layers/dustHaze"
                ),
                new Category(
                    "earthquakes",
                    "Earthquakes",
                    "https://eonet.gsfc.nasa.gov/api/v3/categories/earthquakes",
                    "Related to all manner of shaking and displacement. Certain aftermath of earthquakes may also be found under landslides and floods.",
                    "https://eonet.gsfc.nasa.gov/api/v3/layers/earthquakes"
                ),
                new Category(
                    "floods",
                    "Floods",
                    "https://eonet.gsfc.nasa.gov/api/v3/categories/floods",
                    "Related to aspects of actual flooding--e.g., inundation, water extending beyond river and lake extents.",
                    "https://eonet.gsfc.nasa.gov/api/v3/layers/floods"
                ),
                new Category(
                    "landslides",
                    "Landslides",
                    "https://eonet.gsfc.nasa.gov/api/v3/categories/landslides",
                    "Related to landslides and variations thereof: mudslides, avalanche.",
                    "https://eonet.gsfc.nasa.gov/api/v3/layers/landslides"
                ),
                new Category(
                    "manmade",
                    "Manmade",
                    "https://eonet.gsfc.nasa.gov/api/v3/categories/manmade",
                    "Events that have been human-induced and are extreme in their extent.",
                    "https://eonet.gsfc.nasa.gov/api/v3/layers/manmade"
                ),
                new Category(
                    "seaLakeIce",
                    "Sea and Lake Ice",
                    "https://eonet.gsfc.nasa.gov/api/v3/categories/seaLakeIce",
                    "Related to all ice that resides on oceans and lakes, including sea and lake ice (permanent and seasonal) and icebergs.",
                    "https://eonet.gsfc.nasa.gov/api/v3/layers/seaLakeIce"
                ),
                new Category(
                    "severeStorms",
                    "Severe Storms",
                    "https://eonet.gsfc.nasa.gov/api/v3/categories/severeStorms",
                    "Related to the atmospheric aspect of storms (hurricanes, cyclones, tornadoes, etc.). Results of storms may be included under floods, landslides, etc.",
                    "https://eonet.gsfc.nasa.gov/api/v3/layers/severeStorms"
                ),
                new Category(
                    "snow",
                    "Snow",
                    "https://eonet.gsfc.nasa.gov/api/v3/categories/snow",
                    "Related to snow events, particularly extreme/anomalous snowfall in either timing or extent/depth.",
                    "https://eonet.gsfc.nasa.gov/api/v3/layers/snow"
                ),
                new Category(
                    "tempExtremes",
                    "Temperature Extremes",
                    "https://eonet.gsfc.nasa.gov/api/v3/categories/tempExtremes",
                    "Related to anomalous land temperatures, either heat or cold.",
                    "https://eonet.gsfc.nasa.gov/api/v3/layers/tempExtremes"
                ),
                new Category(
                    "volcanoes",
                    "Volcanoes",
                    "https://eonet.gsfc.nasa.gov/api/v3/categories/volcanoes",
                    "Related to both the physical effects of an eruption (rock, ash, lava) and the atmospheric (ash and gas plumes).",
                    "https://eonet.gsfc.nasa.gov/api/v3/layers/volcanoes"
                ),
                new Category(
                    "waterColor",
                    "Water Color",
                    "https://eonet.gsfc.nasa.gov/api/v3/categories/waterColor",
                    "Related to events that alter the appearance of water: phytoplankton, red tide, algae, sediment, whiting, etc.",
                    "https://eonet.gsfc.nasa.gov/api/v3/layers/waterColor"
                ),
                new Category(
                    "wildfires",
                    "Wildfires",
                    "https://eonet.gsfc.nasa.gov/api/v3/categories/wildfires",
                    "Wildland fires includes all nature of fire, in forest and plains, as well as those that spread to become urban and industrial fire events. Fires may be naturally caused or manmade.",
                    "https://eonet.gsfc.nasa.gov/api/v3/layers/wildfires"
                )
            };
        }
    }
}
