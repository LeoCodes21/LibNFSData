﻿namespace LibNFSData.Core.Utils
{
    public enum ChunkID : long
    {
        BCHUNK_NULL = 0x00000000,
        BCHUNK_UNKNOWN = 0x13377331,
        
        BCHUNK_SPEED_TEXTURE_PACK_LIST_CHUNKS = 0xb3300000,
        BCHUNK_SPEED_TEXTURE_PACK_LIST_CHUNKS_ANIM = 0xb0300100,
        BCHUNK_SPEED_TEXTURE_PACK_LIST_CHUNKS_COMPRESSED = 0x0003A100,
        BCHUNK_SPEED_ESOLID_LIST_CHUNKS = 0x80134000,
        BCHUNK_SPEED_SCENERY_SECTION = 0x80034100,
        BCHUNK_SPEED_SMOKEABLE_SPAWNER = 0x00034027,
        BCHUNK_TRACKSTREAMER_SECTIONS = 0x00034110,
        BCHUNK_TRACKSTREAMER_1 = 0x00034111,
        BCHUNK_TRACKSTREAMER_2 = 0x00034112,
        BCHUNK_TRACKSTREAMER_3 = 0x00034113,
        BCHUNK_TRACKSTREAMER_7 = 0x00034107,

        BCHUNK_SPEED_BBGANIM_BLOCKHEADER = 0x00037220,
        BCHUNK_SPEED_BBGANIM_KEYFRAMES = 0x00037240,
        BCHUNK_SPEED_BBGANIM_INSTANCE_NODE = 0x00037250,
        BCHUNK_SPEED_BBGANIM_INSTANCE_TREE = 0x00037260,
        BCHUNK_SPEED_BBGANIM_ENDPACKHEADER = 0x00037270,

        BCHUNK_SPEED_ELIGHT_CHUNKS = 0x80135000,
        BCHUNK_SPEED_EMTRIGGER_PACK = 0x80036000,
        BCHUNK_SPEED_EMITTER_LIBRARY = 0x0003bc00,
        BCHUNK_FENG_FONT = 0x00030201,
        BCHUNK_FENG_PACKAGE_COMPRESSED = 0x00030210,
        BCHUNK_FENG_PACKAGE = 0x00030203,
        BCHUNK_ELIGHTS = 0x00135200,
        BCHUNK_CARINFO_ARRAY = 0x00034600,
        BCHUNK_CARINFO_SKININFO = 0x00034601,
        BCHUNK_CARINFO_ANIMHOOKUPTABLE = 0x00034608,
        BCHUNK_CARINFO_ANIMHIDETABLES = 0x00034609,
        BCHUNK_CARINFO_SLOTTYPES = 0x00034607,
        BCHUNK_CARINFO_CARPART = 0x80034602,
        BCHUNK_TRACKINFO = 0x00034201,
        BCHUNK_SUN = 0x00034202,
        BCHUNK_ACIDFX = 0x80035000,
        BCHUNK_ACIDFX_TYPE2 = 0x80035010,
        BCHUNK_ACIDFX_TYPE3 = 0x00035021,
        BCHUNK_ACIDFX_EMITTER = 0x00035020,
        BCHUNK_DIFFICULTYINFO = 0x00034b00,
        BCHUNK_STYLEMOMENTSINFO = 0x00034a07,
        BCHUNK_FEPRESETCARS = 0x00030220,
        BCHUNK_EAGLSKELETONS = 0x00e34009,
        BCHUNK_EAGLANIMATIONS = 0x00e34010,
        BCHUNK_MOVIECATALOG = 0x00039020,
        BCHUNK_BOUNDS = 0x8003b900,
        BCHUNK_EMITTERSYSTEM_TEXTUREPAGE = 0x0003bd00,
        BCHUNK_PCAWEIGHTS = 0xb0300300,
        BCHUNK_COLORCUBE = 0x30300201,
        BCHUNK_ANIMDIRECTORYDATA = 0x80037050,
        BCHUNK_ICECAMERASET = 0x8003b200,
        BCHUNK_ICECAMERASET_TYPE2 = 0x8003B201,
        BCHUNK_ICECAMERASET_TYPE3 = 0x8003b202,
        BCHUNK_ICECAMERASET_TYPE4 = 0x8003b203,
        BCHUNK_SOUNDSTICHS = 0x8003b500,
        BCHUNK_TRACKPATH = 0x80034147,
        BCHUNK_TRACKPOSITIONMARKERS = 0x00034146,
        BCHUNK_VISIBLESECTION = 0x00034158,
        BCHUNK_VISIBLESECTION_TYPE2 = 0x80034150,
        BCHUNK_WEATHERMAN = 0x00034250,
        BCHUNK_QUICKSPLINE = 0x8003b000,
        BCHUNK_PARAMETERMAPS = 0x8003b600,
        BCHUNK_SPEED_SCENERY_SECTION_TYPE2 = 0x80034100,
        BCHUNK_SCENERY = 0x00034108,
        BCHUNK_SCENERYGROUP = 0x00034109,
        BCHUNK_SCENERY_TYPE2 = 0x8003410b,
        BCHUNK_WWORLD = 0x0003b800,
        BCHUNK_CARP_WCOLLISIONPACK = 0x0003b801,
        BCHUNK_EVENTSEQUENCE = 0x8003b810,
        BCHUNK_TRACKPATH_TYPE2 = 0x0003414d,
        BCHUNK_WORLDANIMENTITYDATA = 0x00037080,
        BCHUNK_WORLDANIMTREEMARKER = 0x00037110,
        BCHUNK_WORLDANIMINSTANCEENTRY = 0x00037150,
        BCHUNK_WORLDANIMDIRECTORYDATA = 0x00037090,
        BCHUNK_DDSTEXTURE = 0x30300200,
        BCHUNK_SKINREGIONDATABASE = 0x0003ce12,
        BCHUNK_VINYLMETADATA = 0x0003ce13,
        BCHUNK_ICECAMERAS = 0x0003b200,
        BCHUNK_LANGUAGE = 0x00039000,
        BCHUNK_LANGUAGE2 = 0x0003906B,
        BCHUNK_LANGUAGEHISTOGRAM = 0x00039001,
        BCHUNK_STYLEREWARDCHUNK = 0x00034a08,
        BCHUNK_MAGAZINES = 0x00030230,
        BCHUNK_SMOKEABLES = 0x00034026,
        BCHUNK_CAMERA = 0x00034492,
        BCHUNK_CAMERA_TYPE2 = 0x80034405,
        BCHUNK_CAMERA_TYPE3 = 0x80034425,
        BCHUNK_CAMERA_TYPE4 = 0x80034410,
        BCHUNK_CAMERA_TYPE5 = 0x80034415,
        BCHUNK_CAMERA_TYPE6 = 0x80034420,
        BCHUNK_ELIPSE_TABLE = 0x0003a000,
        BCHUNK_NIS_SCENE_MAPPER_DATA = 0x00034036,
        BCHUNK_TRACKROUTE_MANAGER = 0x00034121,
        BCHUNK_TRACKROUTE_SIGNPOSTS = 0x00034122,
        BCHUNK_TRACKROUTE_TRAFFIC_INTERSECTIONS = 0x00034123,
        BCHUNK_TRACKROUTE_CROSS_TRAFFIC_EMITTERS = 0x00034124,
        BCHUNK_TOPOLOGYTREE = 0x00034130,
        BCHUNK_TOPOLOGYTREE_TYPE2 = 0x00034131,
        BCHUNK_TOPOLOGYTREE_TYPE3 = 0x00034132,
        BCHUNK_TOPOLOGYTREE_TYPE4 = 0x00034133,
        BCHUNK_TOPOLOGYTREE_TYPE5 = 0x00034134,
        BCHUNK_WORLDOBJECTS = 0x0003b300,
        BCHUNK_PERFUPGRADELEVELINFOCHUNK = 0x00034a09,
        BCHUNK_PERFUPGRADEPACKAGECHUNK = 0x00034a0a,
        BCHUNK_WIDEDECALS = 0x00030240,
        BCHUNK_RANKINGLADDERS = 0x00034a03,
        BCHUNK_SUBTITLES = 0x00039010,
        BCHUNK_NISSCENEDATA = 0x00034035,
        BCHUNK_ANIMSCENEDATA = 0x80037020,
        BCHUNK_EVENTSEQUENCE_TYPE2 = 0x0003b811
    }
}