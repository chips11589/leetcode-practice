using ChipsPlayGround;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Xunit;

namespace ChipsTest
{
    public class SolutionTests
    {
        [Theory]
        [InlineData(new int[] { 1, 2, 3, 4, 5, 6, 7 }, 3, new int[] { 5, 6, 7, 1, 2, 3, 4 })]
        [InlineData(new int[] { 1, 2, 3, 4, 5, 6, 7 }, 2, new int[] { 6, 7, 1, 2, 3, 4, 5 })]
        [InlineData(new int[] { 1, 2, 3, 4, 5, 6, 7 }, 1, new int[] { 7, 1, 2, 3, 4, 5, 6 })]
        [InlineData(new int[] { -1, -100, 3, 99 }, 2, new int[] { 3, 99, -1, -100 })]
        [InlineData(new int[] { 1 }, 0, new int[] { 1 })]
        [InlineData(new int[] { 1 }, 1, new int[] { 1 })]
        [InlineData(new int[] { 1, 2 }, 1, new int[] { 2, 1 })]
        [InlineData(new int[] { 1, 2 }, 2, new int[] { 1, 2 })]
        [InlineData(new int[] { 1, 2, 3, 4, 5, 6 }, 2, new int[] { 5, 6, 1, 2, 3, 4 })]
        public void Rotate(int[] input, int k, int[] expected)
        {
            Solution.Rotate(input, k);
            for (int i = 0; i < expected.Length; i++)
            {
                input[i].Should().Be(expected[i]);
            }
        }

        [Theory]
        [InlineData(new int[] { 7, 1, 5, 3, 6, 4 }, 7)]
        [InlineData(new int[] { 1, 2, 3, 4, 5 }, 4)]
        [InlineData(new int[] { 7, 6, 4, 3, 1 }, 0)]
        [InlineData(new int[] { 6, 1, 3, 2, 4, 7 }, 7)]
        [InlineData(new int[] { 2, 4, 1 }, 2)]
        [InlineData(new int[] { 1, 2, 1, 4, 5 }, 5)]
        [InlineData(new int[] { 7, 1, 5, 4, 9, 1, 4, 8 }, 16)]
        [InlineData(new int[] { 2, 1, 2, 0, 1 }, 2)]
        [InlineData(new int[] { 1, 9, 6, 9, 1, 7, 1, 1, 5, 9, 9, 9 }, 25)]
        public void MaxProfit(int[] prices, int profit)
        {
            Solution.MaxProfit(prices).Should().Be(profit);
        }

        [Theory]
        [InlineData(new int[] { 1, 2, 2, 1 }, new int[] { 2, 2 }, new int[] { 2, 2 })]
        [InlineData(new int[] { 4, 9, 5 }, new int[] { 9, 4, 9, 8, 4 }, new int[] { 4, 9 })]
        public void Intersect(int[] nums1, int[] nums2, int[] expected)
        {
            Solution.Intersect(nums1, nums2).Should().BeEquivalentTo(expected);
        }

        [Theory]
        [InlineData(new int[] { 1, 3 }, new int[] { 2 }, 2)]
        [InlineData(new int[] { 1, 2 }, new int[] { 3, 4 }, 2.5)]
        [InlineData(new int[] { 3, 5, 10, 11, 17 }, new int[] { 9, 13, 20, 21, 23, 27 }, 13)]
        [InlineData(new int[] { 2, 3, 5, 8 }, new int[] { 10, 12, 14, 16, 18, 20 }, 11)]
        public void FindMedianSortedArrays(int[] nums1, int[] nums2, double expected)
        {
            Solution.FindMedianSortedArrays(nums1, nums2).Should().Be(expected);
        }

        [Fact]
        public void CountkSpikes()
        {
            //Solution.CountkSpikes(new List<int> { 1, 3, 2, 5, 4 }, 1);
            //Solution.CountkSpikes(new List<int> { 1,2,8,3,7,5,4 }, 2);
            Solution.CountkSpikes(new List<int> { 1, 2, 7, 3, 8, 5, 4 }, 2);
        }

        [Fact]
        public void FindMaxProducts()
        {
            //Solution.FindMaxProducts(new List<int> { 2, 9, 4, 7, 5, 2 });
            Solution.FindMaxProducts(new List<int> { 25, 26, 45, 22, 31, 47, 29, 47, 2, 25, 25 });
        }

        [Theory]
        [InlineData("dd", "aa", "bb", "dd", "aa", "dd", "bb", "dd", "aa", "cc", "bb", "cc", "dd", "cc")]
        public void LongestPalindrome(params string[] words)
        {
            Solution.LongestPalindrome(words).Should().Be(22);
        }

        [Theory]
        [InlineData(new int[] { 1, 20, 10, 1, 12, 40, 10, 8 }, 69)]
        [InlineData(new int[] { 3, 2, 3, 4, 12, 4, 4, 20 }, 38)]
        [InlineData(new int[] { 3, 12, 3, 4, 12, 4, 4, 20 }, 44)]
        public void FindLargestAmountToRob(int[] input, int expected)
        {
            Solution.FindLargestAmountToRob(input).Should().Be(expected);
            Solution.FindLargestAmountToRob2(input).Should().Be(expected);
        }

        [Theory]
        [InlineData(new int[] { 1, 20, 10, 1, 12, 40, 10, 8 }, 69)]
        [InlineData(new int[] { 3, 2, 3, 4, 12, 4, 4, 20 }, 38)]
        [InlineData(new int[] { 3, 12, 3, 4, 12, 4, 4, 20 }, 44)]
        public void FindLargestAmountToRob3(int[] input, int expected)
        {
            Solution.FindLargestAmountToRob3(input).Should().Be(expected);
        }

        [Theory]
        [InlineData(new int[] { 2, 1, 5, 3, 4 })]
        [InlineData(new int[] { 2, 5, 1, 3, 4 })]
        [InlineData(new int[] { 1, 2, 5, 3, 7, 8, 6, 4 })] // 7
        [InlineData(new int[] { 2, 1, 5, 6, 3, 4, 9, 8, 11, 7, 10, 14, 13, 12, 17, 16, 15, 19, 18, 22, 20, 24, 23, 21, 27, 28, 25, 26, 30, 29, 33, 32, 31, 35, 36, 34, 39, 38, 37, 42, 40, 44, 41, 43, 47, 46, 48, 45, 50, 52, 49, 51, 54, 56, 55, 53, 59, 58, 57, 61, 63, 60, 65, 64, 67, 68, 62, 69, 66, 72, 70, 74, 73, 71, 77, 75, 79, 78, 81, 82, 80, 76, 85, 84, 83, 86, 89, 90, 88, 87, 92, 91, 95, 94, 93, 98, 97, 100, 96, 102, 99, 104, 101, 105, 103, 108, 106, 109, 107, 112, 111, 110, 113, 116, 114, 118, 119, 117, 115, 122, 121, 120, 124, 123, 127, 125, 126, 130, 129, 128, 131, 133, 135, 136, 132, 134, 139, 140, 138, 137, 143, 141, 144, 146, 145, 142, 148, 150, 147, 149, 153, 152, 155, 151, 157, 154, 158, 159, 156, 161, 160, 164, 165, 163, 167, 166, 162, 170, 171, 172, 168, 169, 175, 173, 174, 177, 176, 180, 181, 178, 179, 183, 182, 184, 187, 188, 185, 190, 189, 186, 191, 194, 192, 196, 197, 195, 199, 193, 198, 202, 200, 204, 205, 203, 207, 206, 201, 210, 209, 211, 208, 214, 215, 216, 212, 218, 217, 220, 213, 222, 219, 224, 221, 223, 227, 226, 225, 230, 231, 229, 228, 234, 235, 233, 237, 232, 239, 236, 241, 238, 240, 243, 242, 246, 245, 248, 249, 250, 247, 244, 253, 252, 251, 256, 255, 258, 254, 257, 259, 261, 262, 263, 265, 264, 260, 268, 266, 267, 271, 270, 273, 269, 274, 272, 275, 278, 276, 279, 277, 282, 283, 280, 281, 286, 284, 288, 287, 290, 289, 285, 293, 291, 292, 296, 294, 298, 297, 299, 295, 302, 301, 304, 303, 306, 300, 305, 309, 308, 307, 312, 311, 314, 315, 313, 310, 316, 319, 318, 321, 320, 317, 324, 325, 322, 323, 328, 327, 330, 326, 332, 331, 329, 335, 334, 333, 336, 338, 337, 341, 340, 339, 344, 343, 342, 347, 345, 349, 346, 351, 350, 348, 353, 355, 352, 357, 358, 354, 356, 359, 361, 360, 364, 362, 366, 365, 363, 368, 370, 367, 371, 372, 369, 374, 373, 376, 375, 378, 379, 377, 382, 381, 383, 380, 386, 387, 384, 385, 390, 388, 392, 391, 389, 393, 396, 397, 394, 398, 395, 401, 400, 403, 402, 399, 405, 407, 406, 409, 408, 411, 410, 404, 413, 412, 415, 417, 416, 414, 420, 419, 422, 421, 418, 424, 426, 423, 425, 428, 427, 431, 430, 429, 434, 435, 436, 437, 432, 433, 440, 438, 439, 443, 441, 445, 442, 447, 444, 448, 446, 449, 452, 451, 450, 455, 453, 454, 457, 456, 460, 459, 458, 463, 462, 464, 461, 467, 465, 466, 470, 469, 472, 468, 474, 471, 475, 473, 477, 476, 480, 479, 478, 483, 482, 485, 481, 487, 484, 489, 490, 491, 488, 492, 486, 494, 495, 496, 498, 493, 500, 499, 497, 502, 504, 501, 503, 507, 506, 505, 509, 511, 508, 513, 510, 512, 514, 516, 518, 519, 515, 521, 522, 520, 524, 517, 523, 525, 526, 529, 527, 531, 528, 533, 532, 534, 530, 537, 536, 539, 535, 541, 538, 540, 543, 544, 542, 547, 548, 545, 549, 546, 552, 550, 551, 554, 553, 557, 555, 556, 560, 559, 558, 563, 562, 564, 561, 567, 568, 566, 565, 569, 572, 571, 570, 575, 574, 577, 576, 579, 573, 580, 578, 583, 581, 584, 582, 587, 586, 585, 590, 589, 588, 593, 594, 592, 595, 591, 598, 599, 596, 597, 602, 603, 604, 605, 600, 601, 608, 609, 607, 611, 612, 606, 610, 615, 616, 614, 613, 619, 618, 617, 622, 620, 624, 621, 626, 625, 623, 628, 627, 631, 630, 633, 629, 635, 632, 637, 636, 634, 638, 640, 642, 639, 641, 645, 644, 647, 643, 646, 650, 648, 652, 653, 654, 649, 651, 656, 658, 657, 655, 661, 659, 660, 663, 664, 666, 662, 668, 667, 670, 665, 671, 673, 669, 672, 676, 677, 674, 679, 675, 680, 678, 681, 684, 682, 686, 685, 683, 689, 690, 688, 687, 693, 692, 691, 696, 695, 698, 694, 700, 701, 702, 697, 704, 699, 706, 703, 705, 709, 707, 711, 712, 710, 708, 713, 716, 715, 714, 718, 720, 721, 719, 723, 717, 722, 726, 725, 724, 729, 728, 727, 730, 733, 732, 735, 734, 736, 731, 738, 737, 741, 739, 740, 744, 743, 742, 747, 746, 745, 750, 748, 752, 749, 753, 751, 756, 754, 758, 755, 757, 761, 760, 759, 764, 763, 762, 767, 765, 768, 766, 771, 770, 769, 774, 773, 776, 772, 778, 777, 779, 775, 781, 780, 783, 784, 782, 786, 788, 789, 787, 790, 785, 793, 791, 792, 796, 795, 794, 798, 797, 801, 799, 803, 800, 805, 802, 804, 808, 806, 807, 811, 809, 810, 814, 812, 813, 817, 816, 819, 818, 815, 820, 821, 823, 822, 824, 826, 827, 825, 828, 831, 829, 830, 834, 833, 836, 832, 837, 839, 838, 841, 835, 840, 844, 842, 846, 845, 843, 849, 847, 851, 850, 852, 848, 855, 854, 853, 857, 856, 858, 861, 862, 860, 859, 863, 866, 865, 864, 867, 870, 869, 868, 872, 874, 875, 871, 873, 877, 878, 876, 880, 881, 879, 884, 883, 885, 882, 888, 886, 890, 891, 889, 893, 887, 895, 892, 896, 898, 894, 899, 897, 902, 901, 903, 905, 900, 904, 908, 907, 910, 909, 906, 912, 911, 915, 913, 916, 918, 914, 919, 921, 917, 923, 920, 924, 922, 927, 925, 929, 928, 926, 932, 931, 934, 930, 933, 935, 937, 939, 940, 938, 936, 943, 944, 942, 941, 947, 946, 948, 945, 951, 950, 949, 953, 952, 956, 954, 958, 957, 955, 961, 962, 963, 959, 964, 966, 960, 965, 969, 968, 971, 967, 970, 974, 972, 976, 973, 975, 979, 977, 981, 982, 978, 980, 983, 986, 984, 985, 989, 988, 987, 990, 993, 991, 995, 994, 997, 992, 999, 1000, 996, 998 })] // 966
        [InlineData(new int[] { 3, 1, 5, 4, 2, 8, 6, 10, 11, 9, 13, 7, 15, 12, 17, 18, 19, 20, 16, 14, 23, 21, 25, 24, 27, 26, 22, 30, 31, 29, 28, 34, 33, 32, 37, 35, 39, 40, 41, 38, 36, 44, 45, 46, 43, 42, 49, 48, 47, 52, 53, 50, 55, 54, 51, 58, 59, 60, 57, 56, 63, 64, 61, 66, 65, 68, 69, 67, 62, 72, 71, 74, 70, 76, 75, 73, 79, 78, 81, 82, 77, 84, 83, 86, 80, 88, 87, 85, 91, 90, 89, 94, 92, 96, 95, 93, 99, 98, 101, 100, 103, 97, 105, 104, 102, 108, 109, 110, 106, 112, 111, 114, 115, 113, 107, 118, 117, 116, 121, 122, 120, 119, 125, 124, 123, 128, 127, 126, 131, 129, 133, 134, 132, 136, 130, 138, 137, 135, 141, 139, 143, 144, 142, 140, 147, 145, 149, 148, 146, 152, 151, 154, 153, 150, 157, 158, 159, 156, 155, 162, 163, 164, 160, 166, 167, 165, 161, 170, 171, 169, 173, 172, 168, 176, 175, 174, 179, 178, 181, 182, 180, 177, 185, 184, 183, 188, 187, 186, 191, 192, 190, 189, 195, 196, 194, 193, 199, 197, 201, 200, 198, 204, 203, 206, 207, 208, 205, 202, 211, 210, 213, 212, 209, 216, 215, 214, 219, 218, 217, 222, 221, 224, 223, 220, 227, 226, 225, 230, 231, 229, 233, 234, 235, 232, 228, 238, 236, 240, 241, 242, 239, 237, 245, 246, 244, 243, 249, 250, 248, 247, 253, 254, 252, 256, 251, 258, 255, 260, 261, 259, 257, 264, 263, 266, 267, 262, 269, 265, 271, 272, 273, 270, 268, 276, 275, 278, 274, 280, 279, 277, 283, 282, 281, 286, 287, 284, 289, 288, 285, 292, 293, 291, 295, 294, 290, 298, 296, 300, 299, 297, 303, 302, 301, 306, 305, 304, 309, 307, 311, 312, 310, 308, 315, 314, 313, 318, 316, 320, 321, 317, 323, 319, 325, 326, 324, 322, 329, 327, 331, 332, 330, 334, 333, 328, 337, 336, 335, 340, 338, 342, 341, 344, 343, 339, 347, 345, 349, 350, 348, 346, 353, 352, 351, 356, 355, 358, 359, 357, 361, 354, 363, 362, 360, 366, 365, 364, 369, 368, 367, 372, 370, 374, 371, 376, 373, 378, 379, 377, 375, 382, 380, 384, 385, 383, 381, 388, 389, 390, 386, 392, 387, 394, 393, 391, 397, 398, 399, 396, 395, 402, 403, 404, 401, 400, 407, 405, 409, 406, 411, 412, 410, 414, 408, 416, 415, 413, 419, 420, 421, 418, 423, 417, 425, 426, 422, 428, 424, 430, 431, 429, 433, 432, 427, 436, 435, 434, 439, 440, 437, 442, 438, 444, 441, 446, 445, 443, 449, 447, 451, 448, 453, 452, 450, 456, 457, 454, 459, 455, 461, 458, 463, 460, 465, 464, 462, 468, 467, 470, 469, 472, 471, 466, 475, 473, 477, 476, 479, 478, 474, 482, 481, 484, 480, 486, 483, 488, 485, 490, 487, 492, 489, 494, 493, 491, 497, 496, 499, 498, 495, 502, 500, 504, 501, 506, 505, 503, 509, 508, 511, 507, 513, 510, 515, 516, 517, 512, 519, 514, 521, 520, 518, 524, 525, 523, 522, 528, 526, 530, 531, 529, 527, 534, 533, 536, 535, 532, 539, 540, 538, 537, 543, 544, 541, 546, 542, 548, 547, 550, 551, 549, 545, 554, 553, 556, 552, 558, 559, 557, 555, 562, 561, 564, 560, 566, 567, 565, 569, 568, 563, 572, 573, 570, 575, 571, 577, 576, 574, 580, 581, 579, 578, 584, 583, 582, 587, 585, 589, 586, 591, 590, 593, 592, 588, 596, 597, 595, 594, 600, 599, 602, 598, 604, 601, 606, 605, 603, 609, 608, 607, 612, 613, 614, 615, 611, 610, 618, 617, 616, 621, 619, 623, 624, 625, 622, 627, 626, 629, 628, 620, 632, 631, 630, 635, 634, 637, 636, 633, 640, 639, 642, 638, 644, 641, 646, 647, 645, 643, 650, 651, 652, 653, 649, 648, 656, 657, 655, 654, 660, 661, 659, 663, 658, 665, 662, 667, 666, 664, 670, 669, 668, 673, 671, 675, 674, 677, 678, 676, 672, 681, 680, 679, 684, 682, 686, 687, 685, 689, 688, 683, 692, 693, 691, 695, 696, 697, 690, 699, 700, 698, 694, 703, 701, 705, 704, 702, 708, 707, 706, 711, 712, 710, 709, 715, 713, 717, 714, 719, 718, 716, 722, 723, 721, 720, 726, 725, 724, 729, 730, 728, 732, 727, 734, 731, 736, 733, 738, 735, 740, 739, 737, 743, 742, 741, 746, 747, 745, 744, 750, 751, 749, 748, 754, 755, 753, 752, 758, 759, 757, 761, 762, 760, 756, 765, 764, 763, 768, 769, 767, 766, 772, 773, 774, 771, 770, 777, 778, 776, 775, 781, 779, 783, 780, 785, 782, 787, 786, 789, 788, 784, 792, 790, 794, 791, 796, 795, 798, 797, 793, 801, 800, 799, 804, 803, 806, 805, 802, 809, 810, 808, 807, 813, 814, 812, 811, 817, 815, 819, 820, 821, 818, 816, 824, 825, 823, 827, 822, 829, 828, 826, 832, 833, 830, 835, 834, 831, 838, 837, 836, 841, 840, 843, 844, 842, 839, 847, 845, 849, 848, 851, 850, 846, 854, 853, 852, 857, 856, 859, 858, 855, 862, 861, 864, 860, 866, 863, 868, 869, 867, 865, 872, 870, 874, 875, 871, 877, 878, 876, 880, 873, 882, 881, 879, 885, 884, 887, 886, 883, 890, 891, 889, 888, 894, 895, 892, 897, 898, 896, 893, 901, 900, 903, 902, 899, 906, 905, 908, 907, 904, 911, 909, 913, 910, 915, 916, 914, 912, 919, 917, 921, 918, 923, 920, 925, 924, 922, 928, 929, 927, 931, 926, 933, 934, 932, 930, 937, 936, 935, 940, 941, 939, 938, 944, 945, 946, 943, 942, 949, 948, 947, 952, 951, 950, 955, 953, 957, 956, 954, 960, 961, 959, 958, 964, 965, 962, 967, 968, 966, 963, 971, 970, 973, 974, 969, 976, 972, 978, 977, 980, 981, 979, 975, 984, 982, 986, 987, 983, 989, 988, 985, 992, 991, 990, 995, 996, 993, 998, 997, 1000, 999, 994 })] // 1201
        [InlineData(new int[] { 5, 1, 2, 3, 7, 8, 6, 4 })] // Too chaotic
        [InlineData(new int[] { 10, 7, 5, 14, 6, 4, 3, 1, 19, 17, 13, 12, 11, 9, 8, 2, 27, 28, 23, 22, 21, 18, 33, 26, 25, 24, 20, 16, 39, 15, 41, 34, 32, 29, 45, 37, 36, 48, 47, 44, 43, 40, 35, 31, 30, 56, 54, 53, 46, 42, 38, 62, 59, 64, 50, 66, 61, 60, 52, 51, 49, 72, 70, 58, 57, 55, 77, 69, 68, 67, 63, 82, 74, 65, 85, 78, 73, 88, 84, 81, 79, 71, 93, 90, 89, 87, 83, 80, 76, 75, 101, 97, 94, 104, 103, 102, 96, 95, 92, 91, 86, 112, 109, 108, 106, 100, 99, 118, 117, 116, 121, 120, 114, 113, 111, 110, 107, 105, 98, 130, 128, 127, 133, 131, 124, 122, 119, 115, 139, 126, 125, 123, 143, 137, 136, 146, 144, 134, 132, 129, 151, 149, 153, 148, 145, 142, 141, 140, 138, 135, 161, 156, 163, 160, 150, 147, 167, 164, 162, 170, 169, 168, 159, 158, 157, 155, 154, 152, 179, 178, 174, 173, 172, 171, 166, 186, 185, 184, 177, 176, 175, 165, 193, 191, 195, 192, 187, 183, 181, 200, 198, 190, 182, 180, 205, 201, 197, 196, 189, 188, 211, 209, 204, 214, 208, 207, 206, 194, 219, 218, 212, 222, 217, 224, 220, 216, 215, 210, 203, 202, 199, 232, 231, 229, 235, 230, 228, 227, 225, 223, 221, 213, 243, 239, 237, 236, 226, 248, 234, 250, 244, 238, 253, 249, 247, 233, 257, 256, 246, 242, 241, 240, 263, 262, 255, 252, 245, 268, 265, 261, 271, 260, 251, 274, 269, 267, 266, 259, 258, 254, 281, 279, 278, 273, 264, 286, 287, 284, 289, 280, 277, 275, 270, 294, 291, 285, 283, 282, 276, 272, 301, 298, 296, 295, 293, 292, 290, 288, 309, 307, 306, 302, 299, 314, 305, 316, 311, 310, 304, 303, 300, 297, 323, 319, 315, 312, 327, 322, 313, 308, 331, 326, 333, 332, 325, 318, 317, 338, 329, 324, 321, 342, 341, 340, 337, 336, 330, 328, 320, 350, 339, 335, 353, 352, 347, 356, 349, 346, 359, 348, 345, 334, 363, 344, 343, 366, 361, 358, 369, 368, 360, 355, 351, 374, 365, 357, 354, 378, 376, 380, 379, 377, 383, 382, 373, 372, 364, 362, 389, 375, 370, 367, 393, 392, 391, 387, 386, 385, 399, 397, 396, 390, 384, 381, 371, 406, 403, 401, 395, 394, 411, 410, 409, 408, 404, 402, 398, 388, 419, 420, 414, 407, 405, 400, 425, 416, 413, 428, 422, 421, 417, 415, 412, 434, 433, 431, 427, 426, 424, 418, 441, 439, 437, 430, 423, 446, 436, 448, 447, 443, 438, 429, 453, 451, 444, 440, 435, 432, 459, 457, 450, 462, 461, 445, 465, 460, 455, 454, 442, 470, 467, 464, 458, 456, 452, 449, 477, 476, 475, 474, 472, 471, 469, 468, 466, 463, 487, 484, 489, 482, 480, 479, 478, 473, 495, 488, 497, 493, 486, 481, 501, 500, 498, 496, 491, 490, 507, 492, 485, 483, 511, 505, 504, 514, 513, 509, 506, 503, 502, 499, 494, 522, 512, 510, 525, 520, 518, 508, 529, 523, 521, 516, 533, 515, 535, 532, 531, 530, 527, 519, 517, 542, 541, 544, 539, 538, 528, 526, 524, 550, 549, 547, 546, 545, 543, 537, 536, 534, 559, 554, 548, 540, 563, 556, 555, 553, 552, 551, 569, 565, 564, 572, 562, 574, 570, 561, 560, 558, 557, 580, 577, 571, 567, 584, 573, 568, 566, 588, 586, 581, 576, 592, 591, 590, 595, 589, 583, 579, 599, 598, 594, 593, 587, 585, 582, 578, 575, 608, 602, 601, 597, 596, 613, 611, 609, 606, 617, 615, 610, 607, 605, 604, 603, 600, 625, 623, 622, 618, 629, 621, 620, 619, 614, 612, 635, 636, 634, 633, 630, 628, 627, 626, 616, 644, 642, 640, 639, 632, 624, 650, 648, 641, 631, 654, 651, 643, 657, 649, 647, 638, 637, 662, 658, 656, 653, 646, 645, 668, 667, 664, 671, 661, 652, 674, 675, 666, 665, 659, 655, 680, 673, 672, 670, 669, 663, 660, 687, 684, 678, 690, 686, 685, 682, 681, 677, 676, 697, 694, 692, 691, 701, 689, 688, 704, 700, 699, 698, 696, 695, 693, 683, 679, 713, 710, 703, 716, 715, 712, 709, 708, 707, 706, 702, 724, 722, 726, 725, 723, 720, 718, 705, 732, 719, 717, 714, 711, 737, 735, 730, 728, 727, 721, 743, 738, 745, 744, 736, 734, 731, 729, 751, 750, 748, 747, 742, 741, 740, 739, 733, 760, 749, 762, 759, 758, 757, 755, 754, 753, 746, 770, 769, 767, 773, 772, 765, 763, 761, 756, 752, 780, 778, 776, 771, 764, 785, 784, 775, 774, 789, 788, 783, 781, 779, 777, 768, 796, 766, 798, 797, 793, 792, 787, 786, 782, 805, 802, 794, 790, 809, 807, 801, 812, 791, 814, 806, 804, 803, 800, 799, 795, 821, 819, 817, 816, 810, 826, 827, 825, 824, 823, 822, 820, 815, 813, 811, 808, 837, 835, 833, 830, 828, 818, 843, 840, 838, 832, 831, 829, 849, 844, 839, 834, 853, 846, 841, 836, 857, 852, 859, 855, 848, 847, 845, 864, 860, 856, 867, 861, 858, 854, 851, 850, 842, 874, 873, 872, 871, 870, 866, 865, 862, 882, 879, 884, 869, 863, 887, 878, 877, 876, 875, 868, 893, 892, 891, 886, 883, 898, 890, 880, 901, 894, 889, 904, 885, 906, 903, 888, 881, 910, 905, 896, 913, 902, 899, 916, 915, 912, 909, 908, 900, 897, 895, 924, 923, 922, 920, 919, 914, 911, 931, 929, 927, 917, 907, 936, 935, 934, 930, 928, 926, 925, 921, 918, 945, 942, 933, 948, 944, 941, 939, 938, 937, 932, 955, 953, 950, 958, 952, 951, 949, 947, 943, 940, 965, 961, 946, 968, 964, 959, 957, 954, 973, 963, 975, 971, 970, 967, 962, 960, 956, 982, 981, 984, 979, 974, 969, 966, 989, 988, 987, 986, 983, 978, 977, 976, 972, 998, 997, 996, 995, 994, 993, 992, 991, 990, 985, 980 })] // Too chaotic
        public void FindMinimumBribes(int[] input)
        {
            Solution.FindMinimumBribes(input);
        }

        [Theory]
        [InlineData("daBcd", "ABC", "YES")]
        [InlineData("daBacd", "ABC", "YES")]
        [InlineData("daBaCcd", "ABC", "YES")]
        [InlineData("daBadCcd", "ABC", "YES")]
        [InlineData("daBaDCcd", "ABC", "NO")]
        [InlineData("daBaDaCabdcd", "ABC", "NO")]
        [InlineData("KXzQ", "K", "NO")]
        [InlineData("beFgH", "EFG", "NO")]
        [InlineData("beFGHefgg", "EFG", "NO")]
        [InlineData("beFgabcG", "EFG", "YES")]
        [InlineData("beFHg", "EFG", "NO")]
        [InlineData("beFGG", "EFG", "NO")]
        [InlineData("bEfgG", "EFG", "YES")]
        [InlineData("befgFGefg", "EFG", "YES")]
        [InlineData("beFaFGefg", "EFG", "NO")]
        [InlineData("befaFGefg", "EFG", "YES")]
        [InlineData("bEfAFFgG", "EAFFG", "YES")]
        [InlineData("bEffFgG", "EFFG", "YES")]
        [InlineData("bEfAFFAFgG", "EFAFG", "NO")]
        [InlineData("bEfFFgG", "EFFG", "YES")]
        [InlineData("bEffFFgG", "EFFG", "YES")]
        [InlineData("befgEfgG", "EFG", "YES")]
        [InlineData(
            "RDWPJPAMKGRIWAPBZSYWALDBLDOFLWIQPMPLEMCJXKAENTLVYMSJNRJAQQPWAGVcGOHEWQYZDJRAXZOYDMNZJVUSJGKKKSYNCSFWKVNHOGVYULALKEBUNZHERDDOFCYWBUCJGbvqlddfazmmohcewjg",
            "RDPJPAMKGRIWAPBZSYWALDBLOFWIQPMPLEMCJXKAENTLVYMJNRJAQQPWAGVGOHEWQYZDJRAXZOYDMNZJVUSJGKKKSYNCSFWKVNHOGVYULALKEBUNZHERDOFCYWBUCJG",
            "NO")]
        [InlineData(
            "MBQEVZPBjcbswirgrmkkfvfvcpiukuxlnxkkenqp",
            "MBQEVZP",
            "NO")]
        [InlineData(
            "DINVMKSOfsVQByBnCWNKPRFRKMhFRSkNQRBVNTIKNBXRSXdADOSeNDcLWFCERZOLQjEZCEPKXPCYKCVKALNxBADQBFDQUpdqunpelxauyyrwtjpkwoxlrrqbjtxlkvkcajhpqhqeitafcsjxwtttzyhzvh",
            "DINVMKSOVQBBCWNKPRFRKMFRSNQRBVNTIKNBXRSXADOSNDLWFCERZOLQEZCEPKXPCYKCVKALNBADQBFDQU",
            "YES")]
        [InlineData(
            "AQIUQVIPJDKYNEBPXFGVHCMFGvURORPRSTYQYJZCYJDNFRPRYTMZIsNDOJAOAGAEFRCDKUJBhdkedalbwoxxnoyowoxpdlelovibyiwat",
            "AQIUQVIPJDKYNEBPXFGVHCMFGURORPRSTYQYJZCYJDNFRPRYTMZINDOJAOAGAEFRCDKUJB",
            "YES")]
        [InlineData(
            "HCPXJZTDXLWHYKHPPDFYFDJWTAETQLJCIIPVHMZHHOQTKONUHGYVKLXTFTBEMYAWXTCSwNJYALIGMIBDOWKIVStFATDOZCYSUCaATUWORPMTFPKTNHDSFWKRKBrXNBYICOZYDWLLElKKWTFAUSTZKFDCBQNYVcWKDHDMXJGFORwURHISYLBIZSOJXVRVBNPQLRJKIN",
            "HCPXJZTDXLWHYKHPPDFYFDJWTAETQLJCIIPVHMZHHOQTKONUHGYVKLXTFTBEMYAWXTCSNJYALIGMIBDOWKIVSFATDOZCYSUCATUWORPMTFPKTNHDSFWKRKBXNBYICOZYDWLLEKKWTFAUSTZKFDCBQNYVWKDHDMXJGFORURHISYLBIZSOJXVRVBNPQLRJKINIIOYB",
            "NO")]
        [InlineData(
            "IZLAKtDFAITDNWMVQPDShQQFGTRIXDLNBQPZRpuRJMLLPHBMOWrNagJDPPREZSYBHIWKDHLkjPSEUWIVQYUfPPJYKCbPEKCSKBRIAAJTMDPAOLNWSQESOTRQZOFTMTTGTDTrWLPENHXHLDWAFNDZMIFlogtcddtulusydquboxquwmgcji",
            "IZLAKDFAITDNWMVQPDSQQFGTRIXDLNBQPZRRJMLLPHBMOWNJDPPREZSYBHIWKDHLPSEUWIVQYUPPJYKCPEKCSKBRIAAJTMDPAOLNWSQESOTRQZOFTMTTGTDTWLPENHXHLDWAFNDZMIF",
            "YES")]
        [InlineData(
            "KRBPLVCTESRNPTCVNDMPTQYvFAWBGYPQHNXNAESRQMKFDZIEKVNZXSXKCFHQYCMMANPQFHWCEeNGOLWTUXZVMQNDZfRPLUFZcSTRLRYAZUKAZYXCVTNTNScSDFTBJSUKEQKZRDITZUCFVAPLCLTUWAXOnNHPYEOZDGWZPBJQBZEOFAeXTFJDWRHI",
            "KRBPVCTESRNPTCVNDMPTQYFWBGYPQHXNAESRQMFDZIEKVNZXSXKFHQYCMMANPQFHWCNGOLWTUXZVMQNDZRPLUFZSTRLRYAZUAZYXCVTNTNSSDTBJSUKEQKZRDITZUCFVAPCLTUWAXONHPYEOZDGWZPBJQBZEOAXTFJDWRHIPGQVCWODYNNV",
            "NO")]
        [InlineData(
            "WOAECAAVWMSQMIMYMAPEVARGIZCTIVNLAgydhmrxwcjltpjdewxhxrtynyyuyhqwbpkwuqtpwmyhemjxvwoazumyfstoumreirdkwbmepwbrgmyhjgtqeltzxnwhbunvuoejnhfqcikggaqjgsoqhzrmu",
            "WOAECAAVWMSQMIMYMAPEVARGIZCTIVNLA",
            "YES")]
        [InlineData(
            "RMPRWOBYTSjXGVJQPDQEHTWXMOGcHVWKATSWLBWPJBQTYKVHKMFKCYVVJXGLUEZTLSXChGBCAOAMiFEAPPAGWeMXXQAQTFCZGXKOGZLLUWTZDOYVWHIJZEIDOSHPwFWHYXCIZKTjKKVKQNDXMTCCBQMAGVCDPZOXHPSEQYthuqclzletakrqbzmaohadpog",
            "RMPRWOBYTSXGVJQPDQEHTWXMOGHVWKATSWLBWPJBQTYKVHKMFKCYVVJXGLUEZTLSXCGBCAOAMFEAPPAGWMXXQAQTFCZGXKOGZLLUWTZDOYVWHIJZEIDOSHPFWHYXCIZKTKKVKQNDXMTCCBQMAGVCDPZOXHPSEQY",
            "YES")]
        [InlineData(
            "BFZZVHdQYHQEMNEFFRFJTQmNWHFVXRXlGTFNBqWQmyOWYWSTDSTMJRYHjBNTEWADLgHVgGIRGKFQSeCXNFNaIFAXOiQORUDROaNoJPXWZXIAABZKSZYFTDDTRGZXVZZNWNRHMvSTGEQCYAJSFvbqivjuqvuzafvwwifnrlcxgbjmigkms",
            "BFZZVHQYHQEMNEFFRFJTQNWHFVXRXGTFNBWQOWYWSTDSTMJRYHBNTEWADLHVGIRGKFQSCXNFNIFAXOQORUDRONJPXWZXIAABZKSZYFTDDTRGZXVZZNWNRHMSTGEQCYAJSF",
            "YES")]
        public void FindAbbreviation(string a, string b, string expected)
        {
            Solution.FindAbbreviation(a, b).Should().Be(expected);
        }

        [Theory]
        [InlineData(
            10, 2,
            new int[] { 0, 0, 1, 1, 2, 2, 3, 4, 5, 6 },
            new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 0 },
            new int[] { 0, 5, 6, 1, 9, 3, 4, 7, 8 })]
        public void FindOrder(int cityNodes, int company, int[] cityFrom, int[] cityTo, int[] expectedOrder)
        {
            Solution
                .FindOrder(cityNodes, cityFrom.ToList(), cityTo.ToList(), company)
                .Should().BeEquivalentTo(expectedOrder.ToList());
        }

        [Fact]
        public void FindLowestPrice()
        {
            var products = new List<List<string>>();
            var discounts = new List<List<string>>();

            products.Add(new List<string> { "3" });
            products.Add(new List<string> { "48" });
            products.Add(new List<string> { "3" });
            products.Add(new List<string> { "5" });

            discounts.Add(new List<string> { "d0", "1", "1" });
            discounts.Add(new List<string> { "d1", "2", "4" });
            discounts.Add(new List<string> { "d2", "2", "4" });

            Solution.FindLowestPrice(products, discounts);
        }

        [Theory]
        [InlineData(new int[] { 4, 5, 6, 2, 6, 7, 6, 5, 4 }, 19)]
        [InlineData(new int[] { 7, 6, 5, 4, 6, 4, 5, 6, 2 }, 19)]
        [InlineData(new int[] { 4, 5, 6, 6, 6, 7, 6, 5, 4 }, 18)]
        [InlineData(new int[] { 4, 5, 6, 6, 6, 6, 7, 6, 5, 4 }, 19)]
        [InlineData(new int[] { 4, 5, 6, 6, 6, 6, 7, 7, 7, 6, 5, 4 }, 22)]
        public void FindOptimalCandies(int[] arr, int expected)
        {
            Solution.FindOptimalCandies(arr.Length, new List<int>(arr)).Should().Be(expected);
        }

        [Theory]
        [InlineData(new int[] { 3, 7, 4, 6, 5 }, 13)]
        [InlineData(new int[] { 2, 1, 5, 8, 4 }, 11)]
        [InlineData(new int[] { 3, 5, -7, 8, 10 }, 15)]
        [InlineData(new int[] { 10, 7, 4, 6 }, 16)]
        [InlineData(new int[] { -10, -7, -4, 6 }, 6)]
        public void FindMaxSubsetSum(int[] arr, int expected)
        {
            Solution.FindMaxSubsetSum(arr).Should().Be(expected);
        }

        [Theory]
        [InlineData(2, new int[] { 2, 4, 1 }, 2)]
        [InlineData(2, new int[] { 3, 2, 6, 5, 0, 3 }, 7)]
        [InlineData(1, new int[] { 3, 2, 6, 5, 0, 6, 9, 1, 11 }, 11)]
        [InlineData(2, new int[] { 3, 2, 6, 5, 0, 6, 9, 1, 11 }, 19)]
        [InlineData(3, new int[] { 3, 2, 6, 5, 0, 6, 9, 1, 11 }, 23)]
        [InlineData(1, new int[] { 3, 2, 6, 5, 0, 6, 9, 1, 2 }, 9)]
        [InlineData(3, new int[] { 3, 2, 6, 5, 3, 6, 9, 1, 2 }, 11)]
        [InlineData(2, new int[] { 3, 2, 6, 5, 0, 6, 9, 1, 2 }, 13)]
        [InlineData(3, new int[] { 3, 2, 6, 5, 0, 6, 9, 1, 2 }, 14)]
        public void FindMaxProfitIV(int k, int[] arr, int expected)
        {
            Solution.FindMaxProfitIV(k, arr).Should().Be(expected);
        }

        [Theory]
        [InlineData(new int[] { 7, 1, 3, 2, 4, 5, 6 }, 5)]
        [InlineData(new int[] { 2, 7, 3, 1, 4, 5, 6 }, 5)]
        [InlineData(new int[] { 4, 3, 1, 2 }, 3)]
        public void FindMinimumSwaps(int[] arr, int expected)
        {
            Solution.FindMinimumSwaps(arr).Should().Be(expected);
        }

        [Theory]
        [InlineData(3, 2, 1, "1-2|3-1|2-3", 4)]
        [InlineData(6, 2, 5, "1-3|3-4|2-4|1-2|2-3|5-6", 12)]
        [InlineData(6, 2, 1, "1-3|3-4|2-4|1-2|2-3|5-6", 8)]
        [InlineData(5, 6, 1, "1-2|1-3|1-4", 15)]
        public void BuildRoadsAndLibraries(int n, int c_lib, int c_road, string s_links, int expected)
        {
            var cities = new List<List<int>>();
            var links = s_links.Split('|');
            foreach (var link in links)
            {
                cities.Add(new List<int>(link.Split('-').Select(s => Convert.ToInt32(s))));
            }

            Solution
                .BuildRoadsAndLibraries(n, c_lib, c_road, cities)
                .Should()
                .Be(expected);

            //var filePath = Directory.GetCurrentDirectory() + "\\TestData\\roads-and-libs.txt";
            //using (StreamReader reader = new(filePath))
            //{
            //    int q = Convert.ToInt32(reader.ReadLine().Trim());

            //    for (int qItr = 0; qItr < q; qItr++)
            //    {
            //        string[] firstMultipleInput = reader.ReadLine().TrimEnd().Split(' ');

            //        n = Convert.ToInt32(firstMultipleInput[0]);

            //        int m = Convert.ToInt32(firstMultipleInput[1]);

            //        c_lib = Convert.ToInt32(firstMultipleInput[2]);

            //        c_road = Convert.ToInt32(firstMultipleInput[3]);

            //        List<List<int>> cities = new List<List<int>>();

            //        for (int i = 0; i < m; i++)
            //        {
            //            cities.Add(reader.ReadLine().TrimEnd().Split(' ').ToList().Select(citiesTemp => Convert.ToInt32(citiesTemp)).ToList());
            //        }

            //        long result = Solution.BuildRoadsAndLibraries(n, c_lib, c_road, cities);
            //    }
            //}
        }

        [Theory]
        [InlineData(4, 3, "1 2|1 3|4 2", "1 2 1 1", 1, 1)]
        [InlineData(4, 3, "1 3|1 2|4 2", "1 2 1 1", 1, 1)]
        [InlineData(4, 3, "1 3|1 2|4 2", "1 2 1 1", 2, -1)]
        [InlineData(5, 4, "1 2|1 3|2 4|3 5", "1 2 3 3 2", 2, 3)]
        public void FindShortest(int n, int m, string edges, string colourIds, int matchingColour, int expected)
        {
            var edgeArr = edges.Split('|');
            var graphFrom = new List<int>(m);
            var graphTo = new List<int>(m);
            var ids = colourIds.Split(' ').Select(item => Convert.ToInt64(item)).ToArray();

            foreach (var edgeStr in edgeArr)
            {
                var points = edgeStr.Split(' ');
                graphFrom.Add(Convert.ToInt32(points[0]));
                graphTo.Add(Convert.ToInt32(points[1]));
            }

            Solution
                .FindShortest(n, graphFrom.ToArray(), graphTo.ToArray(), ids, matchingColour)
                .Should().Be(expected);
        }

        [Theory]
        [InlineData("abba", 4)]
        [InlineData("abcd", 0)]
        [InlineData("cdcd", 5)]
        [InlineData("ifailuhkqq", 3)]
        [InlineData("kkkk", 10)]
        public void FindSherlockAndAnagrams(string s, int expected)
        {
            Solution
                .FindSherlockAndAnagrams(s)
                .Should().Be(expected);
        }

        [Theory]
        [InlineData("2 3 4 2 3 6 8 4 5", 5, 2)]
        [InlineData("1 2 3 4 4", 4, 0)]
        [InlineData("1 1 2 4 2", 4, 0)]
        [InlineData("1 1 2 4 3", 4, 1)]
        [InlineData("1 1 3 4 3", 4, 0)]
        [InlineData("1 1 3 4 4", 4, 1)]
        [InlineData("1 1 3 4 4 5", 5, 0)]
        public void FindActivityNotifications(string expenditures, int d, int expected)
        {
            Solution
                .FindActivityNotificationsV3(expenditures.Split(' ').Select(int.Parse).ToList(), d)
                .Should().Be(expected);
        }

        [Theory]
        [InlineData("activity-notifications.txt", "activity-notifications-2.txt")]
        public void FindActivityNotificationsLargeTestData(params string[] testFileNames)
        {
            foreach (var fileName in testFileNames)
            {
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "TestData", fileName);

                using (var stream = new StreamReader(filePath))
                {
                    var firstLineParts = stream.ReadLine().Split(' ');
                    var d = int.Parse(firstLineParts[1]);
                    var expected = int.Parse(firstLineParts[2]);
                    var expenditures = stream.ReadLine();

                    Solution
                        .FindActivityNotificationsV3(expenditures.Split(' ').Select(int.Parse).ToList(), d)
                        .Should().Be(expected);
                }
            }
        }

        [Theory]
        [InlineData(new[] { 1, 1 }, new[] { 2, 2 }, new[] { 3, 2 }, new[] { 1, 1 }, new[] { 1, 1 }, new[] { 2, 1 }, new[] { 3, 2 }, new[] { 0, 1 })]
        [InlineData(new[] { 1, 5 }, new[] { 1, 6 }, new[] { 3, 2 }, new[] { 1, 10 }, new[] { 1, 10 }, new[] { 1, 6 }, new[] { 2, 5 }, new[] { 3, 2 }, new[] { 0, 1 })]
        [InlineData(new[] { 1, 5 }, new[] { 1, 5 }, new[] { 1, 5 }, new[] { 3, 3 }, new[] { 1 })]
        [InlineData(new[] { 1, 5 }, new[] { 1, 5 }, new[] { 1, 5 }, new[] { 3, 2 }, new[] { 0 })]
        [InlineData(new[] { 2, 5 }, new[] { 3, 1 }, new[] { 3, 1 }, new[] { 0, 0 })]
        [InlineData(new[] { 1, 5 }, new[] { 1, 5 }, new[] { 2, 5 }, new[] { 1, 5 }, new[] { 3, 2 }, new[] { 1 })]
        [InlineData(new[] { 1, 5 }, new[] { 2, 5 }, new[] { 2, 5 }, new[] { 1, 5 }, new[] { 3, 1 }, new[] { 1 })]
        [InlineData(new[] { 1, 5 }, new[] { 1, 4 }, new[] { 2, 5 }, new[] { 2, 5 }, new[] { 3, 1 }, new[] { 1 })]
        [InlineData(new[] { 1, 5 }, new[] { 1, 4 }, new[] { 2, 5 }, new[] { 2, 4 }, new[] { 3, 1 }, new[] { 0 })]
        public void FreqQuery(params int[][] queries)
        {
            var input = new List<List<int>>();
            for (int i = 0; i < queries.Length - 1; i++)
            {
                input.Add(new List<int>(2));

                foreach (var j in queries[i])
                {
                    input[i].Add(j);
                }
            }

            var expected = queries.Last().ToList();

            Solution
                .FreqQuery(input)
                .Should().BeEquivalentTo(expected);
        }

        [Fact]
        public void SortingWithComparator()
        {
            Solution.SortingWithComparator();
        }
    }
}
