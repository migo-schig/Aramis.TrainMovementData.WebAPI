var express = require('express');
var cors = require('cors')
var app = express();


// enable cors for all requests
app.use(cors())


// config for your database
const sql = require("mssql/msnodesqlv8");
const config = {
    database: "testKutscher",
    server: "Fincenz00",
    driver: "msnodesqlv8",
    options: {
        useUtc: true,
        useUTF8: true,
        trustedConnection: true
    }
};


var dbConnect = new sql.connect(config,
    function (err) {
        if (err) {
            console.log("Error while connecting database: " + err)
        } else {
            console.log("connected to database: " + config.server)
        }
    }
)

var server = app.listen(5000, function () {
    console.log('Server is running..');
});


app.get('/geo', function (req, res) {
    // create Request object
    var request = new sql.Request();
    // query to the database and get the records
    console.log('select * from geo_DB');
    request.query('select * from geo_DB', function (err, recordset) {
        if (err) console.log(err)
        // send records as a response
        res.send(recordset.recordset);
    });
});

// app.get('/zlm/betriebsstelle/:bst', function (req, res) {
//     let betriebsstelle = req.params.bst;
//     // create Request object
//     var request = new sql.Request();
//     // query to the database and get the records
//     query = 'select distinct zugnummer from ZLM where betriebsstelle_kurz = \'' + betriebsstelle + '\''
//     console.log(query)
//     request.query(query, function (err, recordset) {
//         if (err) console.log(err)
//         // send records as a response
//         res.send(recordset);
//     });
// });

app.get('/trainnumbers/', function (req, res) {
    let dateStart = req.query.dateStart;
    let dateEnd = req.query.dateEnd;
    let betriebsstelle = req.query.betriebsstelle;
    // create Request object
    var request = new sql.Request();
    // query to the database and get the records
    query = 'select distinct zugnummer from ZLM where REPLACE(betriebsstelle_kurz, \' \', \'\') = REPLACE( \'' + betriebsstelle + ' \', \' \', \'\') and date between \'' + dateStart + '\' and \'' + dateEnd + '\''
    console.log(query)
    request.query(query, function (err, recordset) {
        if (err) console.log(err)
        // send records as a response
        res.send(recordset.recordset);
    });
});


app.get('/zls/daterange', function (req, res) {
    let dateStart = req.query.dateStart;
    let dateEnd = req.query.dateEnd;

    // create Request object
    var request = new sql.Request();
    // query to the database and get the records
    query = 'select zugnummer from ZLS where date >= \'' + dateStart + '\' and date <= \'' + dateEnd + '\'';
    console.log(query)
    request.query(query, function (err, recordset) {
        if (err) console.log(err)
        // send records as a response
        let arr = [];
        recordset.recordset.forEach(entry => {
            arr.push(entry.zugnummer);
        })
        res.send(arr);
    });
});
app.get('/zls/zugnummern/daterange/zugnummer', function (req, res) {
    let dateStart = req.query.dateStart;
    let dateEnd = req.query.dateEnd;
    let zugnummer = req.query.zugnummer;

    // create Request object
    var request = new sql.Request();
    // query to the database and get the records
    query = 'select zugnummer from ZLS where date >= \'' + dateStart + '\' and date <= \'' + dateEnd + '\' and CAST(zugnummer AS varchar(16)) LIKE \'%' + zugnummer + '%\'';
    console.log(query)
    request.query(query, function (err, recordset) {
        if (err) console.log(err)
        // send records as a response
        let arr = [];
        recordset.recordset.forEach(entry => {
            arr.push(entry.zugnummer);
        })
        res.send(arr);
    });
});

// app.get('/geo/zugnummer-daterange/', function (req, res) {
//     let dateStart = req.query.dateStart;
//     let dateEnd = req.query.dateEnd;
//     let zugnummer = req.query.zugnummer;
//     if (zugnummer === '') {
//         query = 'select * from geo_DB geo ' +
//             'WHERE EXISTS(' +
//             'select * from ZLM zlm' +
//             ' where date between \'' + dateStart + '\' and \'' +  dateEnd  + '\' and zlm.betriebsstelle_kurz = geo.betriebsstelle_kurz)'
//     }
//     else {
//         query = 'select * from geo_DB geo ' +
//             'WHERE EXISTS(' +
//             'select * from ZLM zlm' +
//             ' where zugnummer = ' + zugnummer + ' and date between \'' + dateStart + '\' and \'' +  dateEnd  + '\' and zlm.betriebsstelle_kurz = geo.betriebsstelle_kurz)'
//     }
//     var request = new sql.Request();
//
//     console.log(query)
//     request.query(query, function (err, recordset) {
//         if (err) console.log(err)
//         // send records as a response
//         res.send(recordset);
//     });
// });

app.get('/geo/zugnummer/daterange/bst/', function (req, res) {
    let dateStart = req.query.dateStart;
    let dateEnd = req.query.dateEnd;
    let zugnummer = req.query.zugnummer;
    let bst = req.query.bst;
    if (zugnummer === '') {
        if (bst === '') {
            //only date
            query = 'select * from geo_DB geo ' +
                'WHERE EXISTS(' +
                'select * from ZLM zlm' +
                ' where date between \'' + dateStart + '\' and \'' + dateEnd + '\' and REPLACE(zlm.betriebsstelle_kurz, \' \', \'\') = REPLACE(geo.betriebsstelle_kurz, \' \', \'\'))'
        } else {
            // bst and date
            query = 'select * from geo_DB geo ' +
                'WHERE EXISTS(' +
                'select * from ZLM zlm' +
                ' where geo.    betriebsstelle_kurz = \'' + bst + '\' and date between \'' + dateStart + '\' and \'' + dateEnd + '\' and REPLACE(zlm.betriebsstelle_kurz, \' \', \'\') = REPLACE(geo.betriebsstelle_kurz, \' \', \'\'))'
        }
    } else if (bst === '') {
        // zugnummer and date
        query = 'select * from geo_DB geo ' +
            'WHERE EXISTS(' +
            'select * from ZLM zlm' +
            ' where zugnummer = ' + zugnummer + ' and date between \'' + dateStart + '\' and \'' + dateEnd + '\' and REPLACE(zlm.betriebsstelle_kurz, \' \', \'\') = REPLACE(geo.betriebsstelle_kurz, \' \', \'\'))'
    } else {
        // zugnummer and date and bst
        query = 'select * from geo_DB geo ' +
            'WHERE EXISTS(' +
            'select * from ZLM zlm' +
            ' where zugnummer = ' + zugnummer + ' and REPLACE(betriebsstelle_kurz, \' \', \'\') = \'' + bst + '\' and date between \'' + dateStart + '\' and \'' + dateEnd + '\' and REPLACE(zlm.betriebsstelle_kurz, \' \', \'\') = REPLACE(geo.betriebsstelle_kurz, \' \', \'\'))'
    }
    var request = new sql.Request();

    console.log(query)
    request.query(query, function (err, recordset) {
        if (err) console.log(err)
        // send records as a response
        res.send(recordset.recordset);
    });
});


app.get('/zls/zugnummer/date', function (req, res) {
    let dateEnd = req.query.dateEnd;
    let zugnummer = req.query.zugnummer;
    var request = new sql.Request();
    if (zugnummer === '') {
        res.send([]);
    } else {
        query = 'select * from ZLS where zugnummer = ' + zugnummer + ' and date = \'' + dateEnd + '\'';
        console.log(query)
        request.query(query, function (err, recordset) {
            if (err) console.log(err)
            // send records as a response
            res.send(recordset.recordset);
        });
    }

});

app.get('/fz/:zls_id', function (req, res) {
    let zls_id = req.params.zls_id;
    var request = new sql.Request();
    query = 'select * from FZ where zls_id = ' + zls_id
    console.log(query)
    request.query(query, function (err, recordset) {
        if (err) console.log(err)
        // send records as a response
        res.send(recordset.recordset);
    });
});

app.get('/geo/bst/:bst', function (req, res) {
        let bst = req.params.bst;
        var request = new sql.Request();
        query = 'select * from geo_DB where betriebsstelle like \'%' + bst + '%\' or betriebsstelle_kurz like \'%' + bst + '%\''
        console.log(query);
        request.query(query, function (err, recordset) {
            if (err) console.log(err);
            res.send(recordset.recordset);
        })

    }
)

app.get('/zlm/zls/:zlsId', function (req, res) {
        let zlsId = req.params.zlsId;
        var request = new sql.Request();
        query = 'select * from ZLM where zls_id = ' + zlsId + ' order by sollzeit'
        console.log(query);
        request.query(query, function (err, recordset) {
            if (err) console.log(err);
            res.send(recordset.recordset);
        })
    }
)

app.get('/geo/bst-zls-id/:zlsId', function (req, res) {
        let zlsId = req.params.zlsId;
        var request = new sql.Request();
        query = 'select betriebsstelle_kurz from ZLM where zls_id = ' + zlsId + ' order by sollzeit'
        console.log(query);
        request.query(query, function (err, recordset) {
            if (err) console.log(err);
            let arr = [];
            recordset.recordset.forEach(entry => {
                arr.push(entry.betriebsstelle_kurz);
            })
            res.send(arr);
        })
    }
)

// select zlm.betriebsstelle_kurz, zlm.betriebsstelle, latitude, longitude from ZLM zlm  Join geo_DB geo on zlm.betriebsstelle_kurz = geo.betriebsstelle_kurz where zls_id = 4700 order by sollzeit

app.get('/geo/zls/:zlsId', function (req, res) {
        let zlsId = req.params.zlsId;
        var request = new sql.Request();
        query = 'select zlm.betriebsstelle_kurz, zlm.betriebsstelle, latitude, longitude from ZLM zlm  Join geo_DB geo on REPLACE(zlm.betriebsstelle_kurz, \' \', \'\') = REPLACE(geo.betriebsstelle_kurz, \' \', \'\') where zls_id = ' + zlsId + ' order by sollzeit'
        console.log(query);
        request.query(query, function (err, recordset) {
            if (err) console.log(err);
            res.send(recordset.recordset);
        })
    }
)


app.get('/fz/teilstrecke/:zlsId', function (req, res) {
        let zlsId = req.params.zlsId;
        var request = new sql.Request();
        console.log(zlsId);
        console.log('get teilsstrecke')
        query = 'create table teilstrecke(\n' +
            '\tbetriebsstelle_kurz varchar(16),\n' +
            '\tfIn varchar(32),\n' +
            '\tfOut varchar(32),\n' +
            '\tsollzeit time);\n' +
            'insert into teilstrecke(betriebsstelle_kurz, fIn, sollzeit)\n' +
            'select betriebsstelle_kurz, fIn, sollzeit from\n' +
            '\t(select betriebsstelle_kurz, fahrzeug as fIn, sollzeit from\n' +
            '\t\t(select * from\n' +
            '\t\t\tZLM zlm where zls_id = ' + zlsId + ' \n' +
            '\t\t) as zlm left join \n' +
            '\t\t\t(select * from\n' +
            '\t\t\t\tFZ where zls_id = ' + zlsId + '\n' +
            '\t\t\t) as fz \n' +
            '\t\t\ton \n' +
            '\t\t\tzlm.betriebsstelle_kurz = fz.von_betriebsstelle) as rein\n' +
            '\n' +
            'insert into teilstrecke(betriebsstelle_kurz, fOut, sollzeit)\n' +
            'select betriebsstelle_kurz, fOut, sollzeit from\n' +
            '\t(select betriebsstelle_kurz, fahrzeug as fOut, sollzeit from\n' +
            '\t\t(select * from\n' +
            '\t\t\tZLM zlm where zls_id = ' + zlsId + '\n' +
            '\t\t) as zlm left join \n' +
            '\t\t\t(select * from\n' +
            '\t\t\t\tFZ where zls_id = ' + zlsId + '\n' +
            '\t\t\t) as fz \n' +
            '\t\t\ton \n' +
            '\t\t\tzlm.betriebsstelle_kurz = fz.bis_betriebsstelle) as raus\n' +
            'select distinct * from teilstrecke ts inner join geo_DB geo on ts.betriebsstelle_kurz = geo.betriebsstelle_kurz order by sollzeit\n' +
            'drop table teilstrecke'
        console.log(query);
        request.query(query, function (err, recordset) {
            if (err) console.log(err);
            res.send(recordset.recordset);
        })
    }
)

// select distinct zuggattungen from ZLS where zuggattungen LIKE '%rex%'
app.get('/zls/zugGattungen/:zugGattungen', function (req, res) {
        let zugGattungen = req.params.zugGattungen;
        var request = new sql.Request();
        query = 'select distinct zuggattungen from ZLS where zuggattungen LIKE \'%' + zugGattungen + '%\';'
        console.log(query);
        request.query(query, function (err, recordset) {
            if (err) console.log(err);
            res.send(recordset.recordset);
        })
    }
)
app.get('/zls/besteller/:besteller', function (req, res) {
        let besteller = req.params.besteller;
        var request = new sql.Request();
        query = 'select distinct besteller1 from ZLS where besteller1 LIKE \'%' + besteller + '%\';'
        console.log(query);
        request.query(query, function (err, recordset) {
            if (err) console.log(err);
            res.send(recordset.recordset);
        })
    }
)
app.get('/zls/betreiber/:betreiber', function (req, res) {
        let betreiber = req.params.betreiber;
        var request = new sql.Request();
        query = 'select distinct betreiber1 from ZLS where betreiber1 LIKE \'%' + betreiber + '%\';'
        console.log(query);
        request.query(query, function (err, recordset) {
            if (err) console.log(err);
            res.send(recordset.recordset);
        })
    }
)
app.get('/zls/traktionaer/:traktionaer', function (req, res) {
        let traktionaer = req.params.traktionaer;
        var request = new sql.Request();
        query = 'select distinct traktionaer1 from ZLS where traktionaer1 LIKE \'%' + traktionaer + '%\';'
        console.log(query);
        request.query(query, function (err, recordset) {
            if (err) console.log(err);
            res.send(recordset.recordset);
        })
    }
)


app.get('/geo/filters', function (req, res) {
    let zugGattungen = req.query.zugGattungen;
    let besteller = req.query.besteller;
    let betreiber = req.query.betreiber;
    let traktionaer = req.query.traktionaer;
    var request = new sql.Request();

    query = 'SELECT DISTINCT ' +
        '       geo.betriebsstelle, ' +
        '       geo.betriebsstelle_kurz, ' +
        '       geo.latitude, ' +
        '       geo.longitude ' +
        'FROM geo_DB geo ' +
        '     JOIN ZLM zlm ON geo.betriebsstelle_kurz = zlm.betriebsstelle_kurz ' +
        '     JOIN ZLS zls ON zlm.zugnummer = zls.zugnummer ' +
        '                     AND zlm.date = zls.date ' +
        '                     AND zls.besteller1 LIKE \'%' + besteller + '%\' ' +
        '                     AND zls.betreiber1 LIKE \'%' + betreiber + '%\' ' +
        '                     AND zls.traktionaer1 LIKE \'%' + traktionaer + '%\' ' +
        '                     AND zls.zuggattungen LIKE \'%' + zugGattungen + '%\';';

    console.log(query)
    request.query(query, function (err, recordset) {
        if (err) console.log(err)
        // send records as a response
        console.log(recordset.recordset);
        res.send(recordset.recordset);
    });
});


