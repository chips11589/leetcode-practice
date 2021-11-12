'use strict';

console.log('Hello world');

class ChipsApp {
    containsDuplicate(nums) {
        if (typeof nums === 'undefined' || nums.length === 0) {
            return false;
        }

        let dup = false;
        nums = nums.sort(function (a, b) {
            if (a - b === 0) {
                dup = true;
            }
            return a - b;
        });
        return dup;
    }

    singleNumber(nums) {
        nums = nums.sort((a, b) => a - b);
        let maxIndex = nums.length - 1;

        for (let i = 0; i < maxIndex; i += 2) {
            if (nums[i] !== nums[i + 1]) {
                return nums[i];
            }
        }
        return nums[maxIndex];
    }

    intersect(nums1, nums2) {

    };
}

var chipsApp = new ChipsApp();

module.exports = chipsApp;