var assert = require('assert');
var chipsApp = require('../../app');

describe('containsDuplicateTest', function () {
    const dataRows = [
        { input: [1, 2, 3, 1], expected: true },
        { input: [1, 2, 3, 4], expected: false },
        { input: [1, 1, 1, 3, 3, 4, 3, 2, 4, 2], expected: true },
    ];
    for (var i = 0; i < dataRows.length; i++) {
        (function (i) {
            it('containsDuplicate' + i, function () {
                let dataRow = dataRows[i];
                assert.strictEqual(chipsApp.containsDuplicate(dataRow.input), dataRow.expected);
            });
        })(i);
    }
});

describe('singleNumberTest', function () {
    const dataRows = [
        { input: [2, 2, 1], expected: 1 },
        { input: [4, 1, 2, 1, 2], expected: 4 },
        { input: [1], expected: 1 },
    ];
    for (let i = 0; i < dataRows.length; i++) {
        let dataRow = dataRows[i];
        it('singleNumberTest' + i, function () {
            assert.strictEqual(chipsApp.singleNumber(dataRow.input), dataRow.expected);
        });
    }
});
