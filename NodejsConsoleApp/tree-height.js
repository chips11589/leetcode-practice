var Tree = function () {
    this.root = null;
}

Tree.prototype.insert = function (node, data) {
    if (node == null) {
        node = new Node(data);
    }
    else if (data < node.data) {
        node.left = this.insert(node.left, data);
    }
    else {
        node.right = this.insert(node.right, data);
    }

    return node;
}

var Node = function (data) {
    this.data = data;
    this.left = null;
    this.right = null;
}

/* head ends */

function treeHeight(node) {
    return findTreeHeight(node).toString();
}

function findTreeHeight(node) {
    if (node == null) return -1;

    var left = findTreeHeight(node.left);
    var right = findTreeHeight(node.right);

    return left > right ? left + 1 : right + 1;
}

/* tail begins */

function solution() {

    var tree = new Tree();
    var arr = [3, 5, 2, 1, 4, 6, 7];

    for (var i = 0; i < arr.length; i++) {
        tree.root = tree.insert(tree.root, arr[i]);
    }

    var height = treeHeight(tree.root);
    process.stdout.write(height);
}

solution();