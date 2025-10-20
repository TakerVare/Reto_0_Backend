namespace Reto_0_Backend
{
    using System;
    using System.Collections.Generic;
    using System.Security.Cryptography.X509Certificates;
    using Reto_0_Backend.Models;

    public class DataCollectionExample
    {
        public  List<Category> CategoryCollectionList { get; set; }
        public  List<Event> EventCollectionList { get; set; }
        public  List<Feature> FeatureCollectionList { get; set; }
        public  List<Layer> LayerCollectionList { get; set; }

        public  DataCollectionExample()
        {
            // Categorías
            CategoryCollectionList = new List<Category>
            {
                new Category
                {
                    idCategory = "drought",
                    titleCategory = "Drought",
                    linkCategory = "https://eonet.gsfc.nasa.gov/api/v3/categories/drought",
                    descriptionCategory = "Long lasting absence of precipitation affecting agriculture and livestock, and the overall availability of food and water.",
                    layersCategory = "https://eonet.gsfc.nasa.gov/api/v3/layers/drought"
                },
                new Category
                {
                    idCategory = "dustHaze",
                    titleCategory = "Dust and Haze",
                    linkCategory = "https://eonet.gsfc.nasa.gov/api/v3/categories/dustHaze",
                    descriptionCategory = "Related to dust storms, air pollution and other non-volcanic aerosols. Volcano-related plumes shall be included with the originating eruption event.",
                    layersCategory = "https://eonet.gsfc.nasa.gov/api/v3/layers/dustHaze"
                },
                new Category
                {
                    idCategory = "earthquakes",
                    titleCategory = "Earthquakes",
                    linkCategory = "https://eonet.gsfc.nasa.gov/api/v3/categories/earthquakes",
                    descriptionCategory = "Related to all manner of shaking and displacement. Certain aftermath of earthquakes may also be found under landslides and floods.",
                    layersCategory = "https://eonet.gsfc.nasa.gov/api/v3/layers/earthquakes"
                },
                new Category
                {
                    idCategory = "floods",
                    titleCategory = "Floods",
                    linkCategory = "https://eonet.gsfc.nasa.gov/api/v3/categories/floods",
                    descriptionCategory = "Related to aspects of actual flooding--e.g., inundation, water extending beyond river and lake extents.",
                    layersCategory = "https://eonet.gsfc.nasa.gov/api/v3/layers/floods"
                },
                new Category
                {
                    idCategory = "landslides",
                    titleCategory = "Landslides",
                    linkCategory = "https://eonet.gsfc.nasa.gov/api/v3/categories/landslides",
                    descriptionCategory = "Related to landslides and variations thereof: mudslides, avalanche.",
                    layersCategory = "https://eonet.gsfc.nasa.gov/api/v3/layers/landslides"
                },
                new Category
                {
                    idCategory = "manmade",
                    titleCategory = "Manmade",
                    linkCategory = "https://eonet.gsfc.nasa.gov/api/v3/categories/manmade",
                    descriptionCategory = "Events that have been human-induced and are extreme in their extent.",
                    layersCategory = "https://eonet.gsfc.nasa.gov/api/v3/layers/manmade"
                },
                new Category
                {
                    idCategory = "seaLakeIce",
                    titleCategory = "Sea and Lake Ice",
                    linkCategory = "https://eonet.gsfc.nasa.gov/api/v3/categories/seaLakeIce",
                    descriptionCategory = "Related to all ice that resides on oceans and lakes, including sea and lake ice (permanent and seasonal) and icebergs.",
                    layersCategory = "https://eonet.gsfc.nasa.gov/api/v3/layers/seaLakeIce"
                },
                new Category
                {
                    idCategory = "severeStorms",
                    titleCategory = "Severe Storms",
                    linkCategory = "https://eonet.gsfc.nasa.gov/api/v3/categories/severeStorms",
                    descriptionCategory = "Related to the atmospheric aspect of storms (hurricanes, cyclones, tornadoes, etc.). Results of storms may be included under floods, landslides, etc.",
                    layersCategory = "https://eonet.gsfc.nasa.gov/api/v3/layers/severeStorms"
                },
                new Category
                {
                    idCategory = "snow",
                    titleCategory = "Snow",
                    linkCategory = "https://eonet.gsfc.nasa.gov/api/v3/categories/snow",
                    descriptionCategory = "Related to snow events, particularly extreme/anomalous snowfall in either timing or extent/depth.",
                    layersCategory = "https://eonet.gsfc.nasa.gov/api/v3/layers/snow"
                },
                new Category
                {
                    idCategory = "tempExtremes",
                    titleCategory = "Temperature Extremes",
                    linkCategory = "https://eonet.gsfc.nasa.gov/api/v3/categories/tempExtremes",
                    descriptionCategory = "Related to anomalous land temperatures, either heat or cold.",
                    layersCategory = "https://eonet.gsfc.nasa.gov/api/v3/layers/tempExtremes"
                },
                new Category
                {
                    idCategory = "volcanoes",
                    titleCategory = "Volcanoes",
                    linkCategory = "https://eonet.gsfc.nasa.gov/api/v3/categories/volcanoes",
                    descriptionCategory = "Related to both the physical effects of an eruption (rock, ash, lava) and the atmospheric (ash and gas plumes).",
                    layersCategory = "https://eonet.gsfc.nasa.gov/api/v3/layers/volcanoes"
                },
                new Category
                {
                    idCategory = "waterColor",
                    titleCategory = "Water Color",
                    linkCategory = "https://eonet.gsfc.nasa.gov/api/v3/categories/waterColor",
                    descriptionCategory = "Related to events that alter the appearance of water: phytoplankton, red tide, algae, sediment, whiting, etc.",
                    layersCategory = "https://eonet.gsfc.nasa.gov/api/v3/layers/waterColor"
                },
                new Category
                {
                    idCategory = "wildfires",
                    titleCategory = "Wildfires",
                    linkCategory = "https://eonet.gsfc.nasa.gov/api/v3/categories/wildfires",
                    descriptionCategory = "Wildland fires includes all nature of fire, in forest and plains, as well as those that spread to become urban and industrial fire events. Fires may be naturally caused or manmade.",
                    layersCategory = "https://eonet.gsfc.nasa.gov/api/v3/layers/wildfires"
                }
            };

            // Eventos
            EventCollectionList = new List<Event>
            {
                new Event
                {
                    id = "EONET_15547",
                    title = "Tropical Storm Narda",
                    description = null,
                    link = "https://eonet.gsfc.nasa.gov/api/v3/events/EONET_15547",
                    closed = null,
                    categories = new List<Category>
                    {
                        new Category
                        {
                            idCategory = "severeStorms",
                            titleCategory = "Severe Storms"
                        }
                    },
                    sources = new List<Source>
                    {
                        new Source
                        {
                            id = "JTWC",
                            url = "https://www.metoc.navy.mil/jtwc/products/ep1425.tcw"
                        }
                    },
                    geometry = new List<Geometry>
                    {
                        new Geometry("Point", new double[] { -99.4, 14.4 }),
                        new Geometry("Point", new double[] { -100.1, 15.1 }),
                        new Geometry("Point", new double[] { -101, 15.2 }),
                        new Geometry("Point", new double[] { -102.3, 15.6 }),
                        new Geometry("Point", new double[] { -103.3, 16.1 }),
                        new Geometry("Point", new double[] { -104.5, 15.9 }),
                        new Geometry("Point", new double[] { -105.6, 15.7 })
                    }
                },
                new Event
                {
                    id = "EONET_15545",
                    title = "Super Typhoon Ragasa",
                    description = null,
                    link = "https://eonet.gsfc.nasa.gov/api/v3/events/EONET_15545",
                    closed = null,
                    categories = new List<Category>
                    {
                        new Category
                        {
                            idCategory = "severeStorms",
                            titleCategory = "Severe Storms"
                        }
                    },
                    sources = new List<Source>
                    {
                        new Source
                        {
                            id = "JTWC",
                            url = "https://www.metoc.navy.mil/jtwc/products/wp2425.tcw"
                        }
                    },
                    geometry = new List<Geometry>
                    {
                        new Geometry("Point", new double[] { 132.2, 16 }),
                        new Geometry("Point", new double[] { 131.2, 16 }),
                        new Geometry("Point", new double[] { 130.8, 16 }),
                        new Geometry("Point", new double[] { 130.5, 16 }),
                        new Geometry("Point", new double[] { 130, 16.2 }),
                        new Geometry("Point", new double[] { 129.6, 16.3 }),
                        new Geometry("Point", new double[] { 129, 16.8 }),
                        new Geometry("Point", new double[] { 128.6, 17.3 }),
                        new Geometry("Point", new double[] { 127.8, 17.8 }),
                        new Geometry("Point", new double[] { 127, 18 }),
                        new Geometry("Point", new double[] { 126.3, 18.4 }),
                        new Geometry("Point", new double[] { 125.3, 18.9 }),
                        new Geometry("Point", new double[] { 124.1, 19.1 }),
                        new Geometry("Point", new double[] { 122.8, 19.2 }),
                        new Geometry("Point", new double[] { 121.7, 19.3 }),
                        new Geometry("Point", new double[] { 120.5, 19.4 }),
                        new Geometry("Point", new double[] { 119.3, 19.6 }),
                        new Geometry("Point", new double[] { 118.2, 20 }),
                        new Geometry("Point", new double[] { 117.1, 20.4 })
                    }
                },
                new Event
                {
                    id = "EONET_15546",
                    title = "Typhoon Neoguri",
                    description = null,
                    link = "https://eonet.gsfc.nasa.gov/api/v3/events/EONET_15546",
                    closed = null,
                    categories = new List<Category>
                    {
                        new Category
                        {
                            idCategory = "severeStorms",
                            titleCategory = "Severe Storms"
                        }
                    },
                    sources = new List<Source>
                    {
                        new Source
                        {
                            id = "JTWC",
                            url = "https://www.metoc.navy.mil/jtwc/products/wp2525.tcw"
                        }
                    },
                    geometry = new List<Geometry>
                    {
                        new Geometry("Point", new double[] { 162.3, 23.4 }),
                        new Geometry("Point", new double[] { 161.3, 23.7 }),
                        new Geometry("Point", new double[] { 160.3, 24.1 }),
                        new Geometry("Point", new double[] { 159.1, 24.8 }),
                        new Geometry("Point", new double[] { 158, 25.1 }),
                        new Geometry("Point", new double[] { 157, 25.4 }),
                        new Geometry("Point", new double[] { 156.1, 25.9 }),
                        new Geometry("Point", new double[] { 155, 26.3 }),
                        new Geometry("Point", new double[] { 153.8, 26.8 }),
                        new Geometry("Point", new double[] { 152.6, 27.2 }),
                        new Geometry("Point", new double[] { 151.8, 27.7 }),
                        new Geometry("Point", new double[] { 151.4, 28.5 }),
                        new Geometry("Point", new double[] { 150.8, 29.2 }),
                        new Geometry("Point", new double[] { 151, 29.7 }),
                        new Geometry("Point", new double[] { 151.8, 30.2 }),
                        new Geometry("Point", new double[] { 152.6, 30.3 }),
                        new Geometry("Point", new double[] { 152.8, 30.5 }),
                        new Geometry("Point", new double[] { 153.1, 30.5 }),
                        new Geometry("Point", new double[] { 153.3, 30.7 })
                    }
                }
            };

            // Features (GeoJSON)
            FeatureCollectionList = new List<Feature>
            {
                new Feature
                {
                    properties = new List<Property>
                    {
                        new Property
                        {
                            id = "EONET_15547",
                            title = "Tropical Storm Narda",
                            description = null,
                            link = "https://eonet.gsfc.nasa.gov/api/v3/events/EONET_15547/geojson",
                            closed = null,
                            date = DateTime.Parse("2025-09-21T18:00:00Z"),
                            magnitudeValue = 35.00,
                            magnitudeUnit = "kts",
                            categories = new List<Category>
                            {
                                new Category
                                {
                                    idCategory = "severeStorms",
                                    titleCategory = "Severe Storms"
                                }
                            },
                            sources = new List<Source>
                            {
                                new Source
                                {
                                    id = "JTWC",
                                    url = "https://www.metoc.navy.mil/jtwc/products/ep1425.tcw"
                                }
                            }
                        }
                    },
                    geometry = new Geometry("Point", new double[] { -99.4, 14.4 })
                },
                new Feature
                {
                    properties = new List<Property>
                    {
                        new Property
                        {
                            id = "EONET_15547",
                            title = "Tropical Storm Narda",
                            description = null,
                            link = "https://eonet.gsfc.nasa.gov/api/v3/events/EONET_15547/geojson",
                            closed = null,
                            date = DateTime.Parse("2025-09-22T00:00:00Z"),
                            magnitudeValue = 35.00,
                            magnitudeUnit = "kts",
                            categories = new List<Category>
                            {
                                new Category
                                {
                                    idCategory = "severeStorms",
                                    titleCategory = "Severe Storms"
                                }
                            },
                            sources = new List<Source>
                            {
                                new Source
                                {
                                    id = "JTWC",
                                    url = "https://www.metoc.navy.mil/jtwc/products/ep1425.tcw"
                                }
                            }
                        }
                    },
                    geometry = new Geometry("Point", new double[] { -100.1, 15.1 })
                },
                new Feature
                {
                    properties = new List<Property>
                    {
                        new Property
                        {
                            id = "EONET_15545",
                            title = "Super Typhoon Ragasa",
                            description = null,
                            link = "https://eonet.gsfc.nasa.gov/api/v3/events/EONET_15545/geojson",
                            closed = null,
                            date = DateTime.Parse("2025-09-18T18:00:00Z"),
                            magnitudeValue = 35.00,
                            magnitudeUnit = "kts",
                            categories = new List<Category>
                            {
                                new Category
                                {
                                    idCategory = "severeStorms",
                                    titleCategory = "Severe Storms"
                                }
                            },
                            sources = new List<Source>
                            {
                                new Source
                                {
                                    id = "JTWC",
                                    url = "https://www.metoc.navy.mil/jtwc/products/wp2425.tcw"
                                }
                            }
                        }
                    },
                    geometry = new Geometry("Point", new double[] { 132.2, 16 })
                },
                new Feature
                {
                    properties = new List<Property>
                    {
                        new Property
                        {
                            id = "EONET_15546",
                            title = "Typhoon Neoguri",
                            description = null,
                            link = "https://eonet.gsfc.nasa.gov/api/v3/events/EONET_15546/geojson",
                            closed = null,
                            date = DateTime.Parse("2025-09-18T18:00:00Z"),
                            magnitudeValue = 40.00,
                            magnitudeUnit = "kts",
                            categories = new List<Category>
                            {
                                new Category
                                {
                                    idCategory = "severeStorms",
                                    titleCategory = "Severe Storms"
                                }
                            },
                            sources = new List<Source>
                            {
                                new Source
                                {
                                    id = "JTWC",
                                    url = "https://www.metoc.navy.mil/jtwc/products/wp2525.tcw"
                                }
                            }
                        }
                    },
                    geometry = new Geometry("Point", new double[] { 162.3, 23.4 })
                }
            };

            // Layers
            LayerCollectionList = new List<Layer>
            {
                new Layer
                {
                    name = "AIRS_CO_Total_Column_Day",
                    serviceUrl = "https://gibs.earthdata.nasa.gov/wmts/epsg4326/best/wmts.cgi",
                    serviceTypeId = "WMTS_1_0_0",
                    parameters = new List<LayerParameters>
                    {
                        new LayerParameters
                        {
                            TILEMATRIXSET = "2km",
                            FORMAT = "image/png"
                        }
                    }
                },
                new Layer
                {
                    name = "AIRS_CO_Total_Column_Night",
                    serviceUrl = "https://gibs.earthdata.nasa.gov/wmts/epsg4326/best/wmts.cgi",
                    serviceTypeId = "WMTS_1_0_0",
                    parameters = new List<LayerParameters>
                    {
                        new LayerParameters
                        {
                            TILEMATRIXSET = "2km",
                            FORMAT = "image/png"
                        }
                    }
                },
                new Layer
                {
                    name = "AIRS_Dust_Score_Ocean_Day",
                    serviceUrl = "https://gibs.earthdata.nasa.gov/wmts/epsg4326/best/wmts.cgi",
                    serviceTypeId = "WMTS_1_0_0",
                    parameters = new List<LayerParameters>
                    {
                        new LayerParameters
                        {
                            TILEMATRIXSET = "2km",
                            FORMAT = "image/png"
                        }
                    }
                },
                new Layer
                {
                    name = "AIRS_Dust_Score_Ocean_Night",
                    serviceUrl = "https://gibs.earthdata.nasa.gov/wmts/epsg4326/best/wmts.cgi",
                    serviceTypeId = "WMTS_1_0_0",
                    parameters = new List<LayerParameters>
                    {
                        new LayerParameters
                        {
                            FORMAT = "image/png",
                            TILEMATRIXSET = "2km"
                        }
                    }
                },
                new Layer
                {
                    name = "AIRS_Prata_SO2_Index_Day",
                    serviceUrl = "https://gibs.earthdata.nasa.gov/wmts/epsg4326/best/wmts.cgi",
                    serviceTypeId = "WMTS_1_0_0",
                    parameters = new List<LayerParameters>
                    {
                        new LayerParameters
                        {
                            TILEMATRIXSET = "2km",
                            FORMAT = "image/png"
                        }
                    }
                },
                new Layer
                {
                    name = "AIRS_Prata_SO2_Index_Night",
                    serviceUrl = "https://gibs.earthdata.nasa.gov/wmts/epsg4326/best/wmts.cgi",
                    serviceTypeId = "WMTS_1_0_0",
                    parameters = new List<LayerParameters>
                    {
                        new LayerParameters
                        {
                            TILEMATRIXSET = "2km",
                            FORMAT = "image/png"
                        }
                    }
                },
                new Layer
                {
                    name = "AIRS_Precipitation_Day",
                    serviceUrl = "https://gibs.earthdata.nasa.gov/wmts/epsg4326/best/wmts.cgi",
                    serviceTypeId = "WMTS_1_0_0",
                    parameters = new List<LayerParameters>
                    {
                        new LayerParameters
                        {
                            TILEMATRIXSET = "2km",
                            FORMAT = "image/png"
                        }
                    }
                }
            };

        }
        
        
    }
}