class Transaction extends React.Component {
    rawMarkup() {
        const md = new Remarkable();
        const rawMarkup = md.render(this.props.children.toString());
        return { __html: rawMarkup };
    }
    render() {
        return (
              <div class="row">
                <div class="col-sm">
                    {this.props.Amount}
                </div>
                <div class="col-sm">
                    <span dangerouslySetInnerHTML={this.rawMarkup()} />
                </div>
                <div class="col-sm">
                    {this.props.Date}
                </div>
              </div>
        );
    }
}
class TransactionList extends React.Component {
    render() {
        let data = Array.from(this.props.data)

        if (data.length == 0)
            return (<div>--</div>);

        const TransactionNodes = data.map(function (transaction, index) {
            return (
                <Transaction Date={transaction.date} Amount={transaction.amount} key={index}>
                    {transaction.cardNumber}
                </Transaction>
            );
        });
        console.log(TransactionNodes);
        return (<div>
                        <div class="row">
                            <div class="col-sm">
                                Amount
                            </div>
                            <div class="col-sm">
                                CardNumber
                            </div>
                            <div class="col-sm">
                                Date
                            </div>
                        </div>
                        {TransactionNodes}
               </div>);
    }
}
class SearchTransactionList extends React.Component {
    constructor(props) {
        super(props);
        this.state = { data: [] };
    }
    componentDidMount() {
        const xhr = new XMLHttpRequest();
        xhr.open('get', this.props.url, true);
        xhr.onload = () => {
            const data = JSON.parse(xhr.responseText);
            this.setState({ data: data });
        };
        xhr.send();
    }
    render() {
        console.log(this.state.data);
        return (
            <div className="TransactionBox">
                <h1>Transactions</h1>
                <TransactionList data={this.state.data} />
            </div>
        );
    }
}


//ReactDOM.render(<SearchTransactionList url="/api/search" />, document.getElementById('ReactContent'));

