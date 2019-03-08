﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Clouder.Server.Contract.Controller;
using Clouder.Server.Prop;
using Microsoft.Azure.Documents.Client;

namespace Clouder.Server.Controller
{
    public class FactoryController : IFactoryController
    {
        private const int MaxItemCount = 100;
        private const string colId = "Factory";
        private static FeedOptions feedOptions = new FeedOptions { MaxItemCount = MaxItemCount };
        public static List<Factory> factories;

        static FactoryController()
        {
            factories = new List<Factory>
            {
                new Factory { FactoryId = 1, Name = "Factory 1",
                    Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit"},
                new Factory { FactoryId = 2, Name = "Factory 2", Description = "E-Commerce"},
                new Factory { FactoryId = 3, Name = "Factory 3", Description = "Ticketing"},
                new Factory { FactoryId = 4, Name = "Factory 4", Description = "Trading"},
                new Factory { FactoryId = 5, Name = "Factory 5", Description = "Media"},
                new Factory { FactoryId = 6, Name = "Factory 6", Description = "Multi-tennant" },
            };
        }

        public void Update(Factory factory)
        {
            factories.Remove(factories.First(f => f.FactoryId == factory.FactoryId));
            factories.Add(factory);
            
            //return await HttpF.Update<Entity.Factory>(req, colId);
        }
    
        public Task<Factory> Get(int id)
        {
            var factory = factories.First(f => f.FactoryId == id);
            if (factory.Diagram != null)
            {
                return Task.FromResult(factory);
            }
            else
            {
                factory.NodeSettings = "[{\"id\":\"SQLn8cDT\",\"componentName\":\"SQLn8cDT\",\"type\":\"SQL\",\"databaseName\":\"Orders\",\"dbScript\":\"table([OrderStatus]) \\n[ \\n  Id       uniqueIdentifier not null  default NewId() PRIMARY KEY, \\n  StatusId int              not null, \\n  Name     varchar(50)      not null \\n]\\n\\ninsert into (StatusId, Name) values \\n{\\n  (1, 'Pending Payment'), \\n  (2, 'Pending Receipt'), \\n  (3, 'Payment Declined'), \\n  (4, 'Processed'), \\n  (5, 'Out Of Stock') \\n} \",\"size\":\"Small\"},{\"id\":\"HTTP RequestRtD6a\",\"componentName\":\"HTTPRequestRtD6a\",\"type\":\"HTTP Request\",\"functionName\":\"processOrder\",\"inputName\":\"order\",\"size\":\"Free\"},{\"id\":\"Straighturtqe\",\"componentName\":\"Straighturtqe\",\"type\":\"Straight\",\"statementType\":\"Insert\",\"outputName\":\"order.Id\",\"dbScript\":\"insert into Hold (OrderId, Expiration) \\nselect [uniqueidentifier]@order.Id, DATEADD(minute, 5, GETDATE()) \"}]";
                factory.Graph = "[{\"source\":\"HTTP RequestRtD6a-5\",\"target\":\"SQLn8cDT-1\",\"connection\":\"Straighturtqe\"}]";
                factory.Diagram = factoryDiagram;
                return Task.FromResult(factory);
            }
        }

        public Task<Factory> Add(Factory factory)
        {
            factory.FactoryId = factories.Count;
            factories.Add(factory);
            return Task.FromResult(factory);
        }

        private const string factoryDiagram = "{\"width\":\"100%\",\"height\":\"800px\",\"pageSettings\":{\"orientation\":\"Landscape\",\"showPageBreaks\":true,\"multiplePage\":true,\"height\":null,\"width\":null,\"background\":{\"source\":\"\",\"color\":\"transparent\"},\"margin\":{\"left\":0,\"right\":0,\"top\":0,\"bottom\":0},\"boundaryConstraints\":\"Infinity\"},\"selectionChange\":{},\"drop\":{},\"collectionChange\":{},\"connectionChange\":{},\"positionChange\":{},\"enableRtl\":false,\"locale\":\"en-US\",\"enablePersistence\":false,\"scrollSettings\":{\"viewPortWidth\":1620,\"viewPortHeight\":799.9921875,\"currentZoom\":1,\"horizontalOffset\":0,\"verticalOffset\":0,\"canAutoScroll\":false},\"rulerSettings\":{\"showRulers\":false},\"backgroundColor\":\"transparent\",\"constraints\":500,\"layout\":{\"type\":\"None\",\"enableAnimation\":true},\"snapSettings\":{\"constraints\":31,\"verticalGridlines\":{\"lineIntervals\":[1.25,18.75,0.25,19.75,0.25,19.75,0.25,19.75,0.25,19.75],\"snapIntervals\":[20],\"lineColor\":\"lightgray\",\"lineDashArray\":\"\"},\"horizontalGridlines\":{\"lineIntervals\":[1.25,18.75,0.25,19.75,0.25,19.75,0.25,19.75,0.25,19.75],\"snapIntervals\":[20],\"lineColor\":\"lightgray\",\"lineDashArray\":\"\"}},\"contextMenuSettings\":{},\"dataSourceSettings\":{\"dataManager\":null,\"crudAction\":{\"read\":\"\"},\"connectionDataSource\":{\"crudAction\":{\"read\":\"\"}}},\"mode\":\"SVG\",\"layers\":[{\"id\":\"default_layer\",\"visible\":true,\"lock\":false,\"objects\":[\"SQLn8cDT\",\"HTTP RequestRtD6a\",\"Straighturtqe\"],\"zIndex\":0}],\"nodes\":[{\"shape\":{\"type\":\"Bpmn\",\"shape\":\"DataSource\",\"annotations\":[],\"activity\":{\"subProcess\":{}}},\"id\":\"SQLn8cDT\",\"container\":null,\"offsetX\":354,\"offsetY\":110.09375,\"visible\":true,\"horizontalAlignment\":\"Left\",\"verticalAlignment\":\"Top\",\"backgroundColor\":\"transparent\",\"borderColor\":\"none\",\"borderWidth\":0,\"rotateAngle\":0,\"pivot\":{\"x\":0.5,\"y\":0.5},\"margin\":{},\"flip\":\"None\",\"wrapper\":{\"actualSize\":{\"width\":100,\"height\":100},\"offsetX\":354,\"offsetY\":110.09375},\"style\":{\"fill\":\"white\",\"strokeColor\":\"black\",\"strokeWidth\":1,\"strokeDashArray\":\"\",\"opacity\":1,\"gradient\":{\"type\":\"None\"}},\"constraints\":5240814,\"zIndex\":0,\"width\":100,\"height\":100,\"annotations\":[],\"ports\":[{\"offset\":{\"x\":0,\"y\":0.2},\"visibility\":1,\"style\":{\"fill\":\"orange\",\"strokeWidth\":2,\"strokeColor\":\"white\",\"opacity\":1,\"strokeDashArray\":\"\"},\"width\":12,\"height\":12,\"shape\":\"Circle\",\"id\":\"B6IwA\",\"margin\":{\"right\":0,\"bottom\":0,\"left\":0,\"top\":0},\"horizontalAlignment\":\"Center\",\"verticalAlignment\":\"Center\",\"constraints\":1},{\"offset\":{\"x\":0,\"y\":0.4},\"visibility\":1,\"style\":{\"fill\":\"orange\",\"strokeWidth\":2,\"strokeColor\":\"white\",\"opacity\":1,\"strokeDashArray\":\"\"},\"width\":12,\"height\":12,\"shape\":\"Circle\",\"id\":\"Kdxvo\",\"margin\":{\"right\":0,\"bottom\":0,\"left\":0,\"top\":0},\"horizontalAlignment\":\"Center\",\"verticalAlignment\":\"Center\",\"constraints\":1},{\"offset\":{\"x\":0,\"y\":0.6},\"visibility\":1,\"style\":{\"fill\":\"orange\",\"strokeWidth\":2,\"strokeColor\":\"white\",\"opacity\":1,\"strokeDashArray\":\"\"},\"width\":12,\"height\":12,\"shape\":\"Circle\",\"id\":\"AN4Db\",\"margin\":{\"right\":0,\"bottom\":0,\"left\":0,\"top\":0},\"horizontalAlignment\":\"Center\",\"verticalAlignment\":\"Center\",\"constraints\":1},{\"offset\":{\"x\":0,\"y\":0.8},\"visibility\":1,\"style\":{\"fill\":\"orange\",\"strokeWidth\":2,\"strokeColor\":\"white\",\"opacity\":1,\"strokeDashArray\":\"\"},\"width\":12,\"height\":12,\"shape\":\"Circle\",\"id\":\"Fv08I\",\"margin\":{\"right\":0,\"bottom\":0,\"left\":0,\"top\":0},\"horizontalAlignment\":\"Center\",\"verticalAlignment\":\"Center\",\"constraints\":1},{\"offset\":{\"x\":1,\"y\":0.2},\"visibility\":1,\"style\":{\"fill\":\"orange\",\"strokeWidth\":2,\"strokeColor\":\"white\",\"opacity\":1,\"strokeDashArray\":\"\"},\"width\":12,\"height\":12,\"shape\":\"Circle\",\"id\":\"Gtggt\",\"margin\":{\"right\":0,\"bottom\":0,\"left\":0,\"top\":0},\"horizontalAlignment\":\"Center\",\"verticalAlignment\":\"Center\",\"constraints\":1},{\"offset\":{\"x\":1,\"y\":0.4},\"visibility\":1,\"style\":{\"fill\":\"orange\",\"strokeWidth\":2,\"strokeColor\":\"white\",\"opacity\":1,\"strokeDashArray\":\"\"},\"width\":12,\"height\":12,\"shape\":\"Circle\",\"id\":\"CoRCT\",\"margin\":{\"right\":0,\"bottom\":0,\"left\":0,\"top\":0},\"horizontalAlignment\":\"Center\",\"verticalAlignment\":\"Center\",\"constraints\":1},{\"offset\":{\"x\":1,\"y\":0.6},\"visibility\":1,\"style\":{\"fill\":\"orange\",\"strokeWidth\":2,\"strokeColor\":\"white\",\"opacity\":1,\"strokeDashArray\":\"\"},\"width\":12,\"height\":12,\"shape\":\"Circle\",\"id\":\"qMdxs\",\"margin\":{\"right\":0,\"bottom\":0,\"left\":0,\"top\":0},\"horizontalAlignment\":\"Center\",\"verticalAlignment\":\"Center\",\"constraints\":1},{\"offset\":{\"x\":1,\"y\":0.8},\"visibility\":1,\"style\":{\"fill\":\"orange\",\"strokeWidth\":2,\"strokeColor\":\"white\",\"opacity\":1,\"strokeDashArray\":\"\"},\"width\":12,\"height\":12,\"shape\":\"Circle\",\"id\":\"eb12V\",\"margin\":{\"right\":0,\"bottom\":0,\"left\":0,\"top\":0},\"horizontalAlignment\":\"Center\",\"verticalAlignment\":\"Center\",\"constraints\":1}],\"isExpanded\":true,\"expandIcon\":{\"shape\":\"None\"},\"inEdges\":[\"Straighturtqe\"],\"outEdges\":[],\"parentId\":\"\",\"processId\":\"\",\"umlIndex\":-1},{\"shape\":{\"type\":\"Bpmn\",\"shape\":\"Activity\",\"activity\":{\"activity\":\"Task\",\"task\":{\"type\":\"User\",\"call\":false,\"compensation\":false,\"loop\":\"None\"},\"subProcess\":{\"type\":\"None\",\"collapsed\":true}},\"annotations\":[]},\"id\":\"HTTP RequestRtD6a\",\"container\":null,\"offsetX\":115,\"offsetY\":133.09375,\"visible\":true,\"horizontalAlignment\":\"Left\",\"verticalAlignment\":\"Top\",\"backgroundColor\":\"transparent\",\"borderColor\":\"none\",\"borderWidth\":0,\"rotateAngle\":0,\"pivot\":{\"x\":0.5,\"y\":0.5},\"margin\":{},\"flip\":\"None\",\"wrapper\":{\"actualSize\":{\"width\":40,\"height\":200},\"offsetX\":115,\"offsetY\":133.09375},\"style\":{\"fill\":\"white\",\"strokeColor\":\"black\",\"strokeWidth\":1,\"strokeDashArray\":\"\",\"opacity\":1,\"gradient\":{\"type\":\"None\"}},\"constraints\":5240814,\"zIndex\":1,\"width\":40,\"height\":200,\"annotations\":[],\"ports\":[{\"offset\":{\"x\":0,\"y\":0.5},\"visibility\":1,\"style\":{\"fill\":\"orange\",\"strokeWidth\":2,\"strokeColor\":\"white\",\"opacity\":1,\"strokeDashArray\":\"\"},\"width\":12,\"height\":12,\"shape\":\"Circle\",\"id\":\"wGOGa\",\"margin\":{\"right\":0,\"bottom\":0,\"left\":0,\"top\":0},\"horizontalAlignment\":\"Center\",\"verticalAlignment\":\"Center\",\"constraints\":1},{\"offset\":{\"x\":1,\"y\":0},\"visibility\":1,\"style\":{\"fill\":\"orange\",\"strokeWidth\":2,\"strokeColor\":\"white\",\"opacity\":1,\"strokeDashArray\":\"\"},\"width\":12,\"height\":12,\"shape\":\"Circle\",\"id\":\"A47wx\",\"margin\":{\"right\":0,\"bottom\":0,\"left\":0,\"top\":0},\"horizontalAlignment\":\"Center\",\"verticalAlignment\":\"Center\",\"constraints\":1},{\"offset\":{\"x\":1,\"y\":0.1},\"visibility\":1,\"style\":{\"fill\":\"orange\",\"strokeWidth\":2,\"strokeColor\":\"white\",\"opacity\":1,\"strokeDashArray\":\"\"},\"width\":12,\"height\":12,\"shape\":\"Circle\",\"id\":\"Mcety\",\"margin\":{\"right\":0,\"bottom\":0,\"left\":0,\"top\":0},\"horizontalAlignment\":\"Center\",\"verticalAlignment\":\"Center\",\"constraints\":1},{\"offset\":{\"x\":1,\"y\":0.2},\"visibility\":1,\"style\":{\"fill\":\"orange\",\"strokeWidth\":2,\"strokeColor\":\"white\",\"opacity\":1,\"strokeDashArray\":\"\"},\"width\":12,\"height\":12,\"shape\":\"Circle\",\"id\":\"IvgiU\",\"margin\":{\"right\":0,\"bottom\":0,\"left\":0,\"top\":0},\"horizontalAlignment\":\"Center\",\"verticalAlignment\":\"Center\",\"constraints\":1},{\"offset\":{\"x\":1,\"y\":0.3},\"visibility\":1,\"style\":{\"fill\":\"orange\",\"strokeWidth\":2,\"strokeColor\":\"white\",\"opacity\":1,\"strokeDashArray\":\"\"},\"width\":12,\"height\":12,\"shape\":\"Circle\",\"id\":\"ikNJT\",\"margin\":{\"right\":0,\"bottom\":0,\"left\":0,\"top\":0},\"horizontalAlignment\":\"Center\",\"verticalAlignment\":\"Center\",\"constraints\":1},{\"offset\":{\"x\":1,\"y\":0.4},\"visibility\":1,\"style\":{\"fill\":\"orange\",\"strokeWidth\":2,\"strokeColor\":\"white\",\"opacity\":1,\"strokeDashArray\":\"\"},\"width\":12,\"height\":12,\"shape\":\"Circle\",\"id\":\"nmutk\",\"margin\":{\"right\":0,\"bottom\":0,\"left\":0,\"top\":0},\"horizontalAlignment\":\"Center\",\"verticalAlignment\":\"Center\",\"constraints\":1},{\"offset\":{\"x\":1,\"y\":0.5},\"visibility\":1,\"style\":{\"fill\":\"orange\",\"strokeWidth\":2,\"strokeColor\":\"white\",\"opacity\":1,\"strokeDashArray\":\"\"},\"width\":12,\"height\":12,\"shape\":\"Circle\",\"id\":\"itifS\",\"margin\":{\"right\":0,\"bottom\":0,\"left\":0,\"top\":0},\"horizontalAlignment\":\"Center\",\"verticalAlignment\":\"Center\",\"constraints\":1},{\"offset\":{\"x\":1,\"y\":0.6},\"visibility\":1,\"style\":{\"fill\":\"orange\",\"strokeWidth\":2,\"strokeColor\":\"white\",\"opacity\":1,\"strokeDashArray\":\"\"},\"width\":12,\"height\":12,\"shape\":\"Circle\",\"id\":\"dBJAn\",\"margin\":{\"right\":0,\"bottom\":0,\"left\":0,\"top\":0},\"horizontalAlignment\":\"Center\",\"verticalAlignment\":\"Center\",\"constraints\":1},{\"offset\":{\"x\":1,\"y\":0.7},\"visibility\":1,\"style\":{\"fill\":\"orange\",\"strokeWidth\":2,\"strokeColor\":\"white\",\"opacity\":1,\"strokeDashArray\":\"\"},\"width\":12,\"height\":12,\"shape\":\"Circle\",\"id\":\"bdX8y\",\"margin\":{\"right\":0,\"bottom\":0,\"left\":0,\"top\":0},\"horizontalAlignment\":\"Center\",\"verticalAlignment\":\"Center\",\"constraints\":1},{\"offset\":{\"x\":1,\"y\":0.8},\"visibility\":1,\"style\":{\"fill\":\"orange\",\"strokeWidth\":2,\"strokeColor\":\"white\",\"opacity\":1,\"strokeDashArray\":\"\"},\"width\":12,\"height\":12,\"shape\":\"Circle\",\"id\":\"p5NGx\",\"margin\":{\"right\":0,\"bottom\":0,\"left\":0,\"top\":0},\"horizontalAlignment\":\"Center\",\"verticalAlignment\":\"Center\",\"constraints\":1},{\"offset\":{\"x\":1,\"y\":0.9},\"visibility\":1,\"style\":{\"fill\":\"orange\",\"strokeWidth\":2,\"strokeColor\":\"white\",\"opacity\":1,\"strokeDashArray\":\"\"},\"width\":12,\"height\":12,\"shape\":\"Circle\",\"id\":\"fuRwB\",\"margin\":{\"right\":0,\"bottom\":0,\"left\":0,\"top\":0},\"horizontalAlignment\":\"Center\",\"verticalAlignment\":\"Center\",\"constraints\":1},{\"offset\":{\"x\":1,\"y\":1},\"visibility\":1,\"style\":{\"fill\":\"orange\",\"strokeWidth\":2,\"strokeColor\":\"white\",\"opacity\":1,\"strokeDashArray\":\"\"},\"width\":12,\"height\":12,\"shape\":\"Circle\",\"id\":\"HLHaB\",\"margin\":{\"right\":0,\"bottom\":0,\"left\":0,\"top\":0},\"horizontalAlignment\":\"Center\",\"verticalAlignment\":\"Center\",\"constraints\":1}],\"isExpanded\":true,\"expandIcon\":{\"shape\":\"None\"},\"inEdges\":[],\"outEdges\":[\"Straighturtqe\"],\"parentId\":\"\",\"processId\":\"\",\"umlIndex\":-1}],\"connectors\":[{\"shape\":{\"type\":\"None\"},\"id\":\"Straighturtqe\",\"type\":\"Straight\",\"sourcePoint\":{\"x\":135,\"y\":113.09},\"targetPoint\":{\"x\":304,\"y\":100.09},\"targetDecorator\":{\"shape\":\"Arrow\",\"style\":{\"fill\":\"orange\",\"strokeColor\":\"orange\",\"strokeWidth\":1,\"strokeDashArray\":\"\",\"opacity\":1,\"gradient\":{\"type\":\"None\"}},\"width\":10,\"height\":10,\"pivot\":{\"x\":0,\"y\":0.5}},\"style\":{\"strokeWidth\":1,\"strokeColor\":\"orange\",\"fill\":\"transparent\",\"strokeDashArray\":\"\",\"opacity\":1,\"gradient\":{\"type\":\"None\"}},\"sourceID\":\"HTTP RequestRtD6a\",\"targetID\":\"SQLn8cDT\",\"flip\":\"None\",\"segments\":[{\"type\":\"Straight\",\"point\":{\"x\":0,\"y\":0}}],\"sourceDecorator\":{\"shape\":\"None\",\"width\":10,\"height\":10,\"pivot\":{\"x\":0,\"y\":0.5},\"style\":{\"fill\":\"black\",\"strokeColor\":\"black\",\"strokeWidth\":1,\"strokeDashArray\":\"\",\"opacity\":1,\"gradient\":{\"type\":\"None\"}}},\"cornerRadius\":0,\"wrapper\":{\"actualSize\":{\"width\":169,\"height\":13},\"offsetX\":219.5,\"offsetY\":106.59},\"annotations\":[],\"zIndex\":2,\"visible\":true,\"constraints\":11838,\"hitPadding\":10,\"sourcePortID\":\"nmutk\",\"targetPortID\":\"Kdxvo\",\"parentId\":\"\"}],\"selectedItems\":{\"nodes\":[],\"connectors\":[],\"wrapper\":null,\"constraints\":16382,\"rotateAngle\":0,\"pivot\":{\"x\":0.5,\"y\":0.5},\"width\":100,\"height\":100,\"offsetX\":354,\"offsetY\":110.09375,\"userHandles\":[]},\"basicElements\":[],\"tooltip\":{\"content\":\"\",\"relativeMode\":\"Object\"},\"commandManager\":{\"commands\":[]},\"tool\":3}";
    }
}
