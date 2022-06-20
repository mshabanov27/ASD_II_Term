from node import Node


class DoubleLinkedList:
    def __init__(self, head=None):
        head_node = Node(head)
        self.head = head_node

    def push(self, new_node):
        current = self.head
        while current.next is not None:
            current = current.next
        current.next = Node(new_node)
        current.next.previous = current

    def insert(self, data, index):
        inserted_node = Node(data)
        current = self.head
        i = 0
        while i < index:
            current = current.next
            i += 1
        inserted_node.next = current
        inserted_node.previous = current.previous
        current.previous.next = inserted_node
        current.previous = inserted_node

    def delete(self, index):
        current = self.head
        i = 0
        while i < index:
            current = current.next
            i += 1
        current.data = None
        if index != 0:
            current.previous.next = current.next
        else:
            self.head = current.next
        if current.next is not None:
            current.next.previous = current.previous

    def check_on_sorted_after_deletion(self):
        if self.length() > 2:
            current_iterator = 0
            current_node = self.head
            counter = 0

            while current_iterator < self.length() - 1 and counter <= 2:
                if current_node.data > current_node.next.data:
                    counter += 1
                current_iterator += 1
                current_node = current_node.next
            if counter <= 2:
                return True
            return False

    def length(self):
        size = 0
        current = self.head
        while current is not None:
            size += 1
            current = current.next
        return size

    def print_list(self):
        current = self.head
        list_nodes = ''
        while current is not None:
            list_nodes += f'{current.data} '
            current = current.next
        print(list_nodes)
