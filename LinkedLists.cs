using System;
using System.Collections.Generic;
using System.Text;

namespace Media_Player {
    class LinkedList <T> {
        private Node <T>  _head;
        private Node <T>  _tail;
        private int  _size = 0;

        public LinkedList() {
            _head = null;
            _tail = null;
            _size = 0;
        }//end constructor

        public int GetSize {
            get{return _size;}
        }
        public LinkedList(T new_data) {
            _head = new Node <T> (new_data);
            _tail = _head;
            _size++;
        }//end constructor

        public void Add(T new_data) {
            if (_head == null) {//then 
                _head = new Node <T>(new_data);
                _tail = _head;
                _size++;
            }else{
                _tail.Next = new Node <T> (new_data);
                _tail = _tail.Next;
                _size++;
            }//end if
        }//end Add()

        public Node <T> Get(int target_index) {
            int current_index = 0;

            // START @ HEAD
            Node <T> current_node = _head;

            // TRAVERSE THE LIST UNTIL THE END
            while (current_node != null) {
                //IF FOUND RETURN
                if (current_index == target_index) {
                    return current_node;
                }//end if

                //GOTO TO NEXT NODE AND INCREMENT INDEX
                current_node = current_node.Next;
                current_index += 1;
            }//end while

            //IF NOT IN LIST THROW EXCEPTION
            throw new IndexOutOfRangeException("index[" + target_index + "] is not present in this Linked List");
        }//end Get()

        public void InsertFront(T new_data) {
            Node <T> new_node = new Node <T>(new_data);
            new_node.Next = _head;
            _size++;
            _head = new_node;
        }//end InsertFront()
        
        public void Insert(int target_index, T new_data) {

            Node <T> new_node = Get(target_index-1);//array
            Node <T> current_node = new_node.Next;
            Node <T> new_insert = new Node <T> (new_data);
            
            new_node.Next = new_insert;
            new_insert.Next = current_node;

            _size++;
            throw new IndexOutOfRangeException("Target index does not exist in List");
        }//end Insert()

        public object Set(int index, T new_data) {
            Node <T> new_node = Get(index-1);
            Node <T> remaining_node = Get(index);
            Node <T> current_node = remaining_node.Next;
            Node <T> new_insert = new Node <T> (new_data);

            new_node.Next = new_insert;
            new_insert.Next = remaining_node.Next;

            return new_node;

            throw new IndexOutOfRangeException("Index range not found.");
        }//end Set()

        public void Clear() {
            Node <T> current_node = _head;
            while(current_node != null) {
                current_node.Data = default;
                current_node.Next = default;
            }
        }

        public bool Contains(object element) {
            Node <T> current_node = _head;
            while(current_node != null) {              
                if(current_node.Data.Equals(element)) {
                    return true;
                }   //end if      
                current_node = current_node.Next;
            }//end while
            return false;
        }//end Contains()

        public object GetFirst() {
            if(_head == null) {
                throw new Exception("Empty List.");
            }
            return  _head.Data;
        }//end GetFirst()

        public object GetLast() {
            if(_head == null) {
                throw new Exception("Empty List.");
            }
            return  _tail.Data;
        }//end GetLast()

        public int IndexOf(object element) {
            int count = 0;
            Node <T>  current_node = _head;
            while(current_node != null) {
                if(current_node.Data.Equals(element)) {
                    return count;
                }   //end if
                current_node = current_node.Next;
                count++;
            }//end while
            return -1;
        }//end IndexOf()

        public int GetCountOF(object element) {
            int index = 0;
            int count = 0;
            Node <T> current_node = _head;
            while(current_node != null) {
                if(current_node.Data.Equals(element)) {
                    count++;
                }   //end if
                 current_node = current_node.Next;
                index++;
            }//end while
            return count;
        }//end GetCountOf()     

        public object Remove(int index) {
            Node <T> current_node = Get(index);
            
            Node <T> node_chain = current_node.Next;
            
            Node <T> previous = Get(index-1);

            previous.Next = node_chain;

            _size--;

            return current_node;
        }//end Remove()

        public object RemoveFirst() {
            //head node
            Node <T> head_node = _head;

            //head data
            object temp = _head.Data;
            //rest of the node chain
            Node <T> node_chain = head_node.Next;
            //clear your head
            head_node = null;
            //but keep the chain
            _head = node_chain;
            
            _size--;
            //return head's data
            return temp;
        }//end RemoveFirst()

        public object RemoveLast() {

            Node <T> current_node = _head;
            while(current_node != null){
                if(current_node.Next == null) {
                    current_node.Data = default;                
                }
            }
            current_node = current_node.Next;
            
            return current_node;
        }//end RemoveLast()

        public bool RemoveValue(object element) {
            int count = 0;
            Node <T> current_node = _head;
            while(current_node != null) {
                if(current_node.Data.Equals(element)) {
                    current_node.Data = default;
                    return true;
                    
                }   //end if
                
                current_node = current_node.Next;
                count++;
            }//end while
            return false;
        }//end RemoveValue()

        public bool RemoveAll(object element) {
            int count = 0;
            Node <T> current_node = _head;
            while(current_node != null) {
                if(current_node.Data.Equals(element)) {
                    current_node.Data = default;
                    return true;
                }   //end if
                current_node = current_node.Next;
                count++;
            }//end while
            return false;
        }//end RemoveAll()

        public object[] ToArray() {
            Node <T> current_node = _head;
            object[] list_array = new object [_size];

            for (int i = 0; i < list_array.Length; i++) {
                list_array[i] = current_node.Data;
                current_node = current_node.Next;
            }
           
            return list_array;
        }//end ToArray()

        public static LinkedList <T> operator+ (LinkedList <T> list1, LinkedList <T> list2) {
            LinkedList <T> new_list = new LinkedList <T>();
            new_list = list1 + list2;
            return new_list;
        }
    }

    class Node <T> {
        //FIELDS
            private T _data;
            private Node<T>   _next;

        //PROPS
            public T Data { 
                get{return _data;}
                set{_data = value;}     
            }//end property

            public Node<T> Next {
                get { return _next; }
                set { _next = value; }
            }//end property

        //CONSTRUCTORS
            public Node(T new_data) {
                _data = new_data;
            }//end constructor

            public Node() {
                _data = default;
            }//end constructor
    }//end class
}
