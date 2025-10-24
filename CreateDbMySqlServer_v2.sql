-- =============================================
-- Script de Creación de Base de Datos EONET
-- MySQL Server
-- (Corregido para Geometries.Id como VARCHAR(50))
-- =============================================

-- Crear la base de datos
DROP DATABASE IF EXISTS EonetDB;
CREATE DATABASE EonetDB CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci;
USE EonetDB;

-- =============================================
-- Tabla: Categories
-- =============================================
CREATE TABLE Categories (
    IdCategory VARCHAR(50) PRIMARY KEY,
    TitleCategory VARCHAR(255) NOT NULL,
    LinkCategory VARCHAR(500),
    DescriptionCategory TEXT,
    LayersCategory VARCHAR(500)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- =============================================
-- Tabla: Sources
-- =============================================
CREATE TABLE Sources (
    Id VARCHAR(50) PRIMARY KEY,
    Url VARCHAR(500) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- =============================================
-- Tabla: Geometries
-- =============================================
CREATE TABLE Geometries (
    Id VARCHAR(50) PRIMARY KEY,
    Type VARCHAR(50),
    Longitude DOUBLE,
    Latitude DOUBLE,
    INDEX idx_type (Type)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- =============================================
-- Tabla: Events
-- =============================================
CREATE TABLE Events (
    Id VARCHAR(50) PRIMARY KEY,
    Title VARCHAR(255) NOT NULL,
    Description TEXT NULL,
    Link VARCHAR(500),
    Closed VARCHAR(50) NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- =============================================
-- Tabla: Properties
-- =============================================
CREATE TABLE Properties (
    PropertyId VARCHAR(50) PRIMARY KEY,
    Title VARCHAR(255),
    Description TEXT NULL,
    Link VARCHAR(500),
    Closed VARCHAR(50) NULL,
    Date DATETIME NOT NULL,
    MagnitudeValue DOUBLE,
    MagnitudeUnit VARCHAR(50),
    INDEX idx_property_id (PropertyId)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- =============================================
-- Tabla: Features
-- =============================================
CREATE TABLE Features (
    Id INT AUTO_INCREMENT PRIMARY KEY,
    FeatureId VARCHAR(50),
    GeometryId VARCHAR(50),
    FOREIGN KEY (GeometryId) REFERENCES Geometries(Id) ON DELETE CASCADE,
    INDEX idx_feature_id (FeatureId)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- =============================================
-- Tabla: LayerParameters
-- =============================================
CREATE TABLE LayerParameters (
    Id INT AUTO_INCREMENT PRIMARY KEY,
    TILEMATRIXSET VARCHAR(50),
    FORMAT VARCHAR(50)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- =============================================
-- Tabla: Layers
-- =============================================
CREATE TABLE Layers (
    Id INT AUTO_INCREMENT PRIMARY KEY,
    Name VARCHAR(255) NOT NULL,
    ServiceUrl VARCHAR(500),
    ServiceTypeId VARCHAR(50),
    INDEX idx_name (Name)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- =============================================
-- Tablas de Relación (Muchos a Muchos)
-- =============================================

-- Relación: Event - Category
CREATE TABLE EventCategories (
    EventId VARCHAR(50),
    CategoryId VARCHAR(50),
    PRIMARY KEY (EventId, CategoryId),
    FOREIGN KEY (EventId) REFERENCES Events(Id) ON DELETE CASCADE,
    FOREIGN KEY (CategoryId) REFERENCES Categories(IdCategory) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- Relación: Event - Source
CREATE TABLE EventSources (
    EventId VARCHAR(50),
    SourceId VARCHAR(50),
    PRIMARY KEY (EventId, SourceId),
    FOREIGN KEY (EventId) REFERENCES Events(Id) ON DELETE CASCADE,
    FOREIGN KEY (SourceId) REFERENCES Sources(Id) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- Relación: Event - Geometry
CREATE TABLE EventGeometries (
    EventId VARCHAR(50),
    GeometryId VARCHAR(50),
    SequenceOrder INT,
    PRIMARY KEY (EventId, GeometryId),
    FOREIGN KEY (EventId) REFERENCES Events(Id) ON DELETE CASCADE,
    FOREIGN KEY (GeometryId) REFERENCES Geometries(Id) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- Relación: Feature - Property
CREATE TABLE FeatureProperties (
    FeatureId INT,
    PropertyId VARCHAR(50),
    PRIMARY KEY (FeatureId, PropertyId),
    FOREIGN KEY (FeatureId) REFERENCES Features(Id) ON DELETE CASCADE,
    FOREIGN KEY (PropertyId) REFERENCES Properties(PropertyId) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- Relación: Property - Category
CREATE TABLE PropertyCategories (
    PropertyId VARCHAR(50),
    CategoryId VARCHAR(50),
    PRIMARY KEY (PropertyId, CategoryId),
    FOREIGN KEY (PropertyId) REFERENCES Properties(PropertyId) ON DELETE CASCADE,
    FOREIGN KEY (CategoryId) REFERENCES Categories(IdCategory) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- Relación: Property - Source
CREATE TABLE PropertySources (
    PropertyId VARCHAR(50),
    SourceId VARCHAR(50),
    PRIMARY KEY (PropertyId, SourceId),
    FOREIGN KEY (PropertyId) REFERENCES Properties(PropertyId) ON DELETE CASCADE,
    FOREIGN KEY (SourceId) REFERENCES Sources(Id) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- Relación: Layer - LayerParameters
CREATE TABLE LayerParametersRelation (
    LayerId INT,
    ParameterId INT,
    PRIMARY KEY (LayerId, ParameterId),
    FOREIGN KEY (LayerId) REFERENCES Layers(Id) ON DELETE CASCADE,
    FOREIGN KEY (ParameterId) REFERENCES LayerParameters(Id) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- =============================================
-- INSERCIÓN DE DATOS
-- =============================================

-- Insertar Categorías
INSERT INTO Categories (IdCategory, TitleCategory, LinkCategory, DescriptionCategory, LayersCategory) VALUES
('drought', 'Drought', 'https://eonet.gsfc.nasa.gov/api/v3/categories/drought', 
 'Long lasting absence of precipitation affecting agriculture and livestock, and the overall availability of food and water.', 
 'https://eonet.gsfc.nasa.gov/api/v3/layers/drought'),
 
('dustHaze', 'Dust and Haze', 'https://eonet.gsfc.nasa.gov/api/v3/categories/dustHaze', 
 'Related to dust storms, air pollution and other non-volcanic aerosols. Volcano-related plumes shall be included with the originating eruption event.', 
 'https://eonet.gsfc.nasa.gov/api/v3/layers/dustHaze'),
 
('earthquakes', 'Earthquakes', 'https://eonet.gsfc.nasa.gov/api/v3/categories/earthquakes', 
 'Related to all manner of shaking and displacement. Certain aftermath of earthquakes may also be found under landslides and floods.', 
 'https://eonet.gsfc.nasa.gov/api/v3/layers/earthquakes'),
 
('floods', 'Floods', 'https://eonet.gsfc.nasa.gov/api/v3/categories/floods', 
 'Related to aspects of actual flooding--e.g., inundation, water extending beyond river and lake extents.', 
 'https://eonet.gsfc.nasa.gov/api/v3/layers/floods'),
 
('landslides', 'Landslides', 'https://eonet.gsfc.nasa.gov/api/v3/categories/landslides', 
 'Related to landslides and variations thereof: mudslides, avalanche.', 
 'https://eonet.gsfc.nasa.gov/api/v3/layers/landslides'),
 
('manmade', 'Manmade', 'https://eonet.gsfc.nasa.gov/api/v3/categories/manmade', 
 'Events that have been human-induced and are extreme in their extent.', 
 'https://eonet.gsfc.nasa.gov/api/v3/layers/manmade'),
 
('seaLakeIce', 'Sea and Lake Ice', 'https://eonet.gsfc.nasa.gov/api/v3/categories/seaLakeIce', 
 'Related to all ice that resides on oceans and lakes, including sea and lake ice (permanent and seasonal) and icebergs.', 
 'https://eonet.gsfc.nasa.gov/api/v3/layers/seaLakeIce'),
 
('severeStorms', 'Severe Storms', 'https://eonet.gsfc.nasa.gov/api/v3/categories/severeStorms', 
 'Related to the atmospheric aspect of storms (hurricanes, cyclones, tornadoes, etc.). Results of storms may be included under floods, landslides, etc.', 
 'https://eonet.gsfc.nasa.gov/api/v3/layers/severeStorms'),
 
('snow', 'Snow', 'https://eonet.gsfc.nasa.gov/api/v3/categories/snow', 
 'Related to snow events, particularly extreme/anomalous snowfall in either timing or extent/depth.', 
 'https://eonet.gsfc.nasa.gov/api/v3/layers/snow'),
 
('tempExtremes', 'Temperature Extremes', 'https://eonet.gsfc.nasa.gov/api/v3/categories/tempExtremes', 
 'Related to anomalous land temperatures, either heat or cold.', 
 'https://eonet.gsfc.nasa.gov/api/v3/layers/tempExtremes'),
 
('volcanoes', 'Volcanoes', 'https://eonet.gsfc.nasa.gov/api/v3/categories/volcanoes', 
 'Related to both the physical effects of an eruption (rock, ash, lava) and the atmospheric (ash and gas plumes).', 
 'https://eonet.gsfc.nasa.gov/api/v3/layers/volcanoes'),
 
('waterColor', 'Water Color', 'https://eonet.gsfc.nasa.gov/api/v3/categories/waterColor', 
 'Related to events that alter the appearance of water: phytoplankton, red tide, algae, sediment, whiting, etc.', 
 'https://eonet.gsfc.nasa.gov/api/v3/layers/waterColor'),
 
('wildfires', 'Wildfires', 'https://eonet.gsfc.nasa.gov/api/v3/categories/wildfires', 
 'Wildland fires includes all nature of fire, in forest and plains, as well as those that spread to become urban and industrial fire events. Fires may be naturally caused or manmade.', 
 'https://eonet.gsfc.nasa.gov/api/v3/layers/wildfires');

-- Insertar Sources
INSERT INTO Sources (Id, Url) VALUES
('JTWC', 'https://www.metoc.navy.mil/jtwc/products/ep1425.tcw'),
('JTWC_WP24', 'https://www.metoc.navy.mil/jtwc/products/wp2425.tcw'),
('JTWC_WP25', 'https://www.metoc.navy.mil/jtwc/products/wp2525.tcw');

-- Insertar Events
INSERT INTO Events (Id, Title, Description, Link, Closed) VALUES
('EONET_15547', 'Tropical Storm Narda', NULL, 'https://eonet.gsfc.nasa.gov/api/v3/events/EONET_15547', NULL),
('EONET_15545', 'Super Typhoon Ragasa', NULL, 'https://eonet.gsfc.nasa.gov/api/v3/events/EONET_15545', NULL),
('EONET_15546', 'Typhoon Neoguri', NULL, 'https://eonet.gsfc.nasa.gov/api/v3/events/EONET_15546', NULL);

-- Insertar EventCategories
INSERT INTO EventCategories (EventId, CategoryId) VALUES
('EONET_15547', 'severeStorms'),
('EONET_15545', 'severeStorms'),
('EONET_15546', 'severeStorms');

-- Insertar EventSources
INSERT INTO EventSources (EventId, SourceId) VALUES
('EONET_15547', 'JTWC'),
('EONET_15545', 'JTWC_WP24'),
('EONET_15546', 'JTWC_WP25');

-- Insertar Geometries para EONET_15547 (Tropical Storm Narda)
INSERT INTO Geometries (Id, Type, Longitude, Latitude) VALUES
('EONET_15547_1', 'Point', -99.4, 14.4),
('EONET_15547_2', 'Point', -100.1, 15.1),
('EONET_15547_3', 'Point', -101, 15.2),
('EONET_15547_4', 'Point', -102.3, 15.6),
('EONET_15547_5', 'Point', -103.3, 16.1),
('EONET_15547_6', 'Point', -104.5, 15.9),
('EONET_15547_7', 'Point', -105.6, 15.7);

-- Insertar EventGeometries para EONET_15547
INSERT INTO EventGeometries (EventId, GeometryId, SequenceOrder) VALUES
('EONET_15547', 'EONET_15547_1', 1),
('EONET_15547', 'EONET_15547_2', 2),
('EONET_15547', 'EONET_15547_3', 3),
('EONET_15547', 'EONET_15547_4', 4),
('EONET_15547', 'EONET_15547_5', 5),
('EONET_15547', 'EONET_15547_6', 6),
('EONET_15547', 'EONET_15547_7', 7);

-- Insertar Geometries para EONET_15545 (Super Typhoon Ragasa)
INSERT INTO Geometries (Id, Type, Longitude, Latitude) VALUES
('EONET_15545_1', 'Point', 132.2, 16),
('EONET_15545_2', 'Point', 131.2, 16),
('EONET_15545_3', 'Point', 130.8, 16),
('EONET_15545_4', 'Point', 130.5, 16),
('EONET_15545_5', 'Point', 130, 16.2),
('EONET_15545_6', 'Point', 129.6, 16.3),
('EONET_15545_7', 'Point', 129, 16.8),
('EONET_15545_8', 'Point', 128.6, 17.3),
('EONET_15545_9', 'Point', 127.8, 17.8),
('EONET_15545_10', 'Point', 127, 18),
('EONET_15545_11', 'Point', 126.3, 18.4),
('EONET_15545_12', 'Point', 125.3, 18.9),
('EONET_15545_13', 'Point', 124.1, 19.1),
('EONET_15545_14', 'Point', 122.8, 19.2),
('EONET_15545_15', 'Point', 121.7, 19.3),
('EONET_15545_16', 'Point', 120.5, 19.4),
('EONET_15545_17', 'Point', 119.3, 19.6),
('EONET_15545_18', 'Point', 118.2, 20),
('EONET_15545_19', 'Point', 117.1, 20.4);

-- Insertar EventGeometries para EONET_15545
INSERT INTO EventGeometries (EventId, GeometryId, SequenceOrder) VALUES
('EONET_15545', 'EONET_15545_1', 1),
('EONET_15545', 'EONET_15545_2', 2),
('EONET_15545', 'EONET_15545_3', 3),
('EONET_15545', 'EONET_15545_4', 4),
('EONET_15545', 'EONET_15545_5', 5),
('EONET_15545', 'EONET_15545_6', 6),
('EONET_15545', 'EONET_15545_7', 7),
('EONET_15545', 'EONET_15545_8', 8),
('EONET_15545', 'EONET_15545_9', 9),
('EONET_15545', 'EONET_15545_10', 10),
('EONET_15545', 'EONET_15545_11', 11),
('EONET_15545', 'EONET_15545_12', 12),
('EONET_15545', 'EONET_15545_13', 13),
('EONET_15545', 'EONET_15545_14', 14),
('EONET_15545', 'EONET_15545_15', 15),
('EONET_15545', 'EONET_15545_16', 16),
('EONET_15545', 'EONET_15545_17', 17),
('EONET_15545', 'EONET_15545_18', 18),
('EONET_15545', 'EONET_15545_19', 19);

-- Insertar Geometries para EONET_15546 (Typhoon Neoguri)
INSERT INTO Geometries (Id, Type, Longitude, Latitude) VALUES
('EONET_15546_1', 'Point', 162.3, 23.4),
('EONET_15546_2', 'Point', 161.3, 23.7),
('EONET_15546_3', 'Point', 160.3, 24.1),
('EONET_15546_4', 'Point', 159.1, 24.8),
('EONET_15546_5', 'Point', 158, 25.1),
('EONET_15546_6', 'Point', 157, 25.4),
('EONET_15546_7', 'Point', 156.1, 25.9),
('EONET_15546_8', 'Point', 155, 26.3),
('EONET_15546_9', 'Point', 153.8, 26.8),
('EONET_15546_10', 'Point', 152.6, 27.2),
('EONET_15546_11', 'Point', 151.8, 27.7),
('EONET_15546_12', 'Point', 151.4, 28.5),
('EONET_15546_13', 'Point', 150.8, 29.2),
('EONET_15546_14', 'Point', 151, 29.7),
('EONET_15546_15', 'Point', 151.8, 30.2),
('EONET_15546_16', 'Point', 152.6, 30.3),
('EONET_15546_17', 'Point', 152.8, 30.5),
('EONET_15546_18', 'Point', 153.1, 30.5),
('EONET_15546_19', 'Point', 153.3, 30.7);

-- Insertar EventGeometries para EONET_15546
INSERT INTO EventGeometries (EventId, GeometryId, SequenceOrder) VALUES
('EONET_15546', 'EONET_15546_1', 1),
('EONET_15546', 'EONET_15546_2', 2),
('EONET_15546', 'EONET_15546_3', 3),
('EONET_15546', 'EONET_15546_4', 4),
('EONET_15546', 'EONET_15546_5', 5),
('EONET_15546', 'EONET_15546_6', 6),
('EONET_15546', 'EONET_15546_7', 7),
('EONET_15546', 'EONET_15546_8', 8),
('EONET_15546', 'EONET_15546_9', 9),
('EONET_15546', 'EONET_15546_10', 10),
('EONET_15546', 'EONET_15546_11', 11),
('EONET_15546', 'EONET_15546_12', 12),
('EONET_15546', 'EONET_15546_13', 13),
('EONET_15546', 'EONET_15546_14', 14),
('EONET_15546', 'EONET_15546_15', 15),
('EONET_15546', 'EONET_15546_16', 16),
('EONET_15546', 'EONET_15546_17', 17),
('EONET_15546', 'EONET_15546_18', 18),
('EONET_15546', 'EONET_15546_19', 19);

-- Insertar Properties para Features
INSERT INTO Properties (PropertyId, Title, Description, Link, Closed, Date, MagnitudeValue, MagnitudeUnit) VALUES
('EONET_15547', 'Tropical Storm Narda', NULL, 'https://eonet.gsfc.nasa.gov/api/v3/events/EONET_15547/geojson', NULL, '2025-09-21 18:00:00', 35.00, 'kts'),
('EONET_15545', 'Super Typhoon Ragasa', NULL, 'https://eonet.gsfc.nasa.gov/api/v3/events/EONET_15545/geojson', NULL, '2025-09-18 18:00:00', 35.00, 'kts'),
('EONET_15546', 'Typhoon Neoguri', NULL, 'https://eonet.gsfc.nasa.gov/api/v3/events/EONET_15546/geojson', NULL, '2025-09-18 18:00:00', 40.00, 'kts');

-- Insertar PropertyCategories
INSERT INTO PropertyCategories (PropertyId, CategoryId) VALUES
('EONET_15547', 'severeStorms'),
('EONET_15545', 'severeStorms'),
('EONET_15546', 'severeStorms');

-- Insertar PropertySources
INSERT INTO PropertySources (PropertyId, SourceId) VALUES
('EONET_15547', 'JTWC'),
('EONET_15545', 'JTWC_WP24'),
('EONET_15546', 'JTWC_WP25');

-- Insertar Features
INSERT INTO Features (FeatureId, GeometryId) VALUES
('EONET_15547_1', 'EONET_15547_1'),
('EONET_15547