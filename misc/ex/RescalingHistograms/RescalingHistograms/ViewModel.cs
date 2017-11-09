using RescalingHistograms.Model;
using System.Collections.ObjectModel;
using System.Windows.Data;

namespace RescalingHistograms
{
    public class ViewModel
    {
        public CompositeCollection Data { get; private set; }

        public ViewModel()
        {
            ObservableCollection<Employee> data1 = new ObservableCollection<Employee>(new Employee[]
            {
                new Employee("Hans", "Programmer"),
                new Employee("Elister", "Programmer"),
                new Employee("Steve", "GUI Designer"),
                new Employee("Stefan", "GUI Designer"),
                new Employee("Joe", "Coffee Getter"),
                new Employee("Julien", "Programmer"),
            });
            ObservableCollection<Machine> data2 = new ObservableCollection<Machine>(new Machine[]
            {
                new Machine("E12", "GreedCorp"),
                new Machine("E11", "GreedCorp"),
                new Machine("F1-MII", "CommerceComp"),
                new Machine("F2-E5", "CommerceComp")
            });
            CompositeCollection coll = new CompositeCollection();
            coll.Add(new CollectionContainer() { Collection = data1 });
            coll.Add(new CollectionContainer() { Collection = data2 });
            Data = coll;
        }
    }
}
